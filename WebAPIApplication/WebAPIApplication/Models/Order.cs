namespace WebAPIApplication.Models;

public class Order : Base
{
  public Guid CustomerId { get; private set; }
  public Guid ProductId { get; private set; }
  public Customer Customer { get; set; } = null!;
  public Product Product { get; set; } = null!;

  public Order(Guid customerId, Guid productId)
  {
    CustomerId = customerId;
    ProductId = productId;

    CreatedAt = DateTime.Now;
  }

}