using System.Text.Json.Serialization;

namespace WebAPIApplication.Models;

public class Shop : Base
{
  public string Name { get; private set; }
  public string Location { get; private set; }

  [JsonIgnore]
  public List<Product> Products { get; set; }

  public Shop(string name, string location)
  {
    Name = name;
    Location = location;

    CreatedAt = DateTime.Now;

    Products = new List<Product>();
  }
}