
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPIApplication.Helper;
using WebAPIApplication.Models;
using WebAPIApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

builder.Services.AddControllers(); builder.Services.AddControllers(config =>
{
  // Add XML Content Negotiation
  config.RespectBrowserAcceptHeader = true;
}).AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
  options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// To fixed the bug: Cannot write DateTime with Kind=Unspecified to PostgreSQL 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IShopService, ShopService>();
builder.Services.AddSingleton<IOrderService, OrderService>();

var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderingDbContext>(option => option.UseNpgsql(defaultConnectionString));
DatabaseHelper.SetConnectionString(defaultConnectionString ?? string.Empty);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));

app.UseAuthorization();

app.MapControllers();

app.Run();
