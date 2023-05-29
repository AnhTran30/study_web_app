using System.ComponentModel.DataAnnotations;

namespace WebAPIApplication.Models.Requests.Customer;

public class CreateCustomerRequest
{
  [Required]
  public string FullName { get; set; } = null!;

  [Required]
  public DateTime DayOfBirth { get; set; }

  [Required]
  public string Email { get; set; } = null!;
}