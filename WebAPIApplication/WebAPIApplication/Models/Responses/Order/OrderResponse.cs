namespace WebAPIApplication.Models.Responses.Order;

public class OrderResponse
{
  public Guid CustomerId { get; set; }
  public Guid ProductId { get; set; }
  public string CustomerName { get; set; } = null!;
  public string CustomerEmail { get; set; } = null!;
  public string ShopName { get; set; } = null!;
  public string ShopLocation { get; set; } = null!;
  public string ProductName { get; set; } = null!;
  public decimal ProductPrice { get; set; }
}