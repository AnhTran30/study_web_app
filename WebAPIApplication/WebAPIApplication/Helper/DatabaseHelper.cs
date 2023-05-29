using Microsoft.EntityFrameworkCore;
using WebAPIApplication.Models;

namespace WebAPIApplication.Helper;

public class DatabaseHelper
{
  private static string _postgresConnection = string.Empty;

  public static void SetConnectionString(string connectionString)
  {
    _postgresConnection = connectionString;
  }

  internal static OrderingDbContext? GetDbContext()
  {
    if (string.IsNullOrEmpty(_postgresConnection)) return null;
    var optionsBuilder = new DbContextOptionsBuilder<OrderingDbContext>()
        .UseNpgsql(connectionString: _postgresConnection);
    return new OrderingDbContext(optionsBuilder.Options);
  }
}