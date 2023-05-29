using Microsoft.EntityFrameworkCore;
using WebAPIApplication.Helper;
using WebAPIApplication.Models;
using WebAPIApplication.Models.Requests.Shop;

namespace WebAPIApplication.Services;

public interface IShopService
{
  Task<List<Shop>> GetAll();
  Task<bool> Create(CreateShopRequest request);
}

public class ShopService : IShopService
{
  public async Task<List<Shop>> GetAll()
  {
    var result = new List<Shop>();
    await using var context = DatabaseHelper.GetDbContext();
    if (context == null) return result;

    try
    {
      result = await context.Shops.AsNoTracking().Where(x => x.DeletedAt == null).ToListAsync();
    }
    catch (Exception ex)
    {
      Console.WriteLine("Exception: {0}", ex.Message);
    }

    return result;
  }

  public async Task<bool> Create(CreateShopRequest request)
  {
    await using var context = DatabaseHelper.GetDbContext();
    if (context == null) return false;
    try
    {
      var existedShop = await context.Shops
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Name == request.Name && x.Location == request.Location);

      if (existedShop != null) return false;

      var newShop = new Shop(request.Name, request.Location);
      await context.Shops.AddAsync(newShop);
      await context.SaveChangesAsync();
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine("Exception: {0}", ex.Message);
      return false;
    }
  }
}