namespace WebAPIApplication.Models;

public class Base
{
  public Guid Id { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public DateTime? DeletedAt { get; set; }
}