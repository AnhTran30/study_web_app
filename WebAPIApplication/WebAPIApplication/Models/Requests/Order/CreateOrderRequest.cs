using System.ComponentModel.DataAnnotations;

namespace WebAPIApplication.Models.Requests.Order;

public class CreateOrderRequest
{
  [Required]
  public Guid CustomerId { get; set; }

  [Required]
  public Guid ProductId { get; set; }
}