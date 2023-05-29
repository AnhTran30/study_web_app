namespace WebAPIApplication.Models;

public class Product : Base
{
  public string Name { get; private set; }
  public decimal Price { get; private set; }
  public Guid ShopId { get; private set; }
  public Shop Shop { get; set; } = null!;

  public Product(string name, decimal price, Guid shopId)
  {
    Name = name;
    Price = price;
    ShopId = shopId;

    CreatedAt = DateTime.Now;
  }

}