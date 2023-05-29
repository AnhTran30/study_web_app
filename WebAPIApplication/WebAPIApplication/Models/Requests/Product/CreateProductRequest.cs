using System.ComponentModel.DataAnnotations;

namespace WebAPIApplication.Models.Requests.Product;

public class CreateProductRequest
{
  [Required]
  public string Name { get; set; } = null!;
  [Required]
  public decimal Price { get; set; }

  [Required]
  public Guid ShopId { get; set; }
}