using System.ComponentModel.DataAnnotations;

namespace WebAPIApplication.Models.Requests.Shop;

public class CreateShopRequest
{
  [Required]
  public string Name { get; set; } = null!;
  [Required]
  public string Location { get; set; } = null!;
}