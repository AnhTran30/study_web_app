using Microsoft.EntityFrameworkCore;
using WebAPIApplication.Helper;
using WebAPIApplication.Models;
using WebAPIApplication.Models.Requests.Order;
using WebAPIApplication.Models.Responses.Order;

namespace WebAPIApplication.Services;

public interface IOrderService
{
  Task<List<OrderResponse>> GetAll();
  Task<bool> CheckValidData();
  Task<bool> Add(CreateOrderRequest request);
}

public class OrderService : IOrderService
{
  public async Task<List<OrderResponse>> GetAll()
  {
    var result = new List<OrderResponse>();

    await using var context = DatabaseHelper.GetDbContext();
    if (context == null) return result;

    try
    {
      var orders = context.Orders.AsNoTracking();
      var shops = context.Shops.AsNoTracking();
      var products = context.Products.AsNoTracking();
      var customers = context.Customers.AsNoTracking();

      var query = from o in orders
                  join cus in customers on o.CustomerId equals cus.Id
                  join p in products on o.ProductId equals p.Id
                  join sh in shops on p.ShopId equals sh.Id
                  where cus.DeletedAt == null &&
                        p.DeletedAt == null &&
                        sh.DeletedAt == null
                  select new OrderResponse
                  {
                    CustomerEmail = cus.Email,
                    CustomerName = cus.FullName,
                    ShopLocation = sh.Location,
                    ShopName = sh.Name,
                    ProductName = p.Name,
                    ProductPrice = p.Price,
                    ProductId = p.Id,
                    CustomerId = cus.Id
                  };

      result = await query.OrderBy(x => x.CustomerEmail)
                            .ThenByDescending(x => x.ShopLocation)
                            .ThenBy(x => x.ProductName)
                            .ToListAsync();
    }
    catch (Exception ex)
    {
      Console.WriteLine("Exception: {0}", ex.Message);
    }

    return result;
  }

  public async Task<bool> CheckValidData()
  {
    await using var context = DatabaseHelper.GetDbContext();
    if (context == null) return false;

    try
    {
      var shops = context.Shops.AsNoTracking().Count();
      if (shops < 3) return false;

      var products = context.Products.AsNoTracking().Count();
      if (products < 30) return false;

      var customers = context.Customers.AsNoTracking().Count();
      if (customers < 30) return false;

      return true;

    }
    catch (Exception ex)
    {
      Console.WriteLine("Exception: {0}", ex.Message);
      return false;
    }
  }

  public async Task<bool> Add(CreateOrderRequest request)
  {
    await using var context = DatabaseHelper.GetDbContext();
    if (context == null) return false;
    try
    {
      var newOrder = new Order(request.CustomerId, request.ProductId);

      await context.Orders.AddAsync(newOrder);
      await context.SaveChangesAsync();
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine("Exception: {0}", ex.Message);
      return false;
    }
  }
}