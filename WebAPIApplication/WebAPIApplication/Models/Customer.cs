using System.Text.Json.Serialization;

namespace WebAPIApplication.Models;

public class Customer : Base
{
  public string FullName { get; private set; } = null!;

  public DateTime DayOfBirth { get; private set; }

  public string Email { get; private set; } = null!;

  [JsonIgnore]
  public List<Order> Orders { get; set; }

  public Customer()
  {
    Orders = new List<Order>();
  }
  public Customer(string fullName, DateTime dateOfBirth, string email)
  {
    FullName = fullName;
    DayOfBirth = dateOfBirth;
    Email = email;

    CreatedAt = DateTime.Now;

    Orders = new List<Order>();
  }
}
