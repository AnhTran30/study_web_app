using Microsoft.EntityFrameworkCore;

namespace WebAPIApplication.Models;

public class OrderingDbContext : DbContext
{

  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Shop> Shops => Set<Shop>();
  public DbSet<Product> Products => Set<Product>();
  public DbSet<Order> Orders => Set<Order>();
  public OrderingDbContext(DbContextOptions<OrderingDbContext> options)
      : base(options)
  {
  }
}