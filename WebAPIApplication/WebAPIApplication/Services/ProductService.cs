using Microsoft.EntityFrameworkCore;
using WebAPIApplication.Helper;
using WebAPIApplication.Models;
using WebAPIApplication.Models.Requests.Product;

namespace WebAPIApplication.Services;

public interface IProductService
{
  Task<List<Product>> GetProducts(Guid? shopId);
  Task<bool> Create(CreateProductRequest request);
}

public class ProductService : IProductService
{
  public async Task<List<Product>> GetProducts(Guid? shopId)
  {
    var result = new List<Product>();
    await using var context = DatabaseHelper.GetDbContext();
    if (context == null) return result;

    try
    {
      var query = context.Products.Include(s => s.Shop)
      .AsNoTracking().Where(x => x.DeletedAt == null);

      if (shopId != null)
        query = query.Where(w => w.ShopId == shopId);

      result = await query.ToListAsync();
    }
    catch (Exception ex)
    {
      Console.WriteLine("Exception: {0}", ex.Message);
    }

    return result;
  }

  public async Task<bool> Create(CreateProductRequest request)
  {
    await using var context = DatabaseHelper.GetDbContext();
    if (context == null) return false;
    try
    {
      var existedProduct = await context.Products
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Name == request.Name && x.ShopId == request.ShopId);

      if (existedProduct != null) return false;

      var newProduct = new Product(request.Name, request.Price, request.ShopId);
      await context.Products.AddAsync(newProduct);
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