using Microsoft.EntityFrameworkCore;
using WebAPIApplication.Helper;
using WebAPIApplication.Models;
using WebAPIApplication.Models.Requests.Customer;

namespace WebAPIApplication.Services;

public interface ICustomerService
{
  Task<List<Customer>> GetAll();
  Task<bool> Create(CreateCustomerRequest request);
}

public class CustomerService : ICustomerService
{
  public async Task<List<Customer>> GetAll()
  {
    var result = new List<Customer>();
    await using var context = DatabaseHelper.GetDbContext();
    if (context == null) return result;

    try
    {
      result = await context.Customers.AsNoTracking()
          .Where(x => x.DeletedAt == null)
          .ToListAsync();
    }
    catch (Exception ex)
    {
      Console.WriteLine("Exception: {0}", ex.Message);
    }

    return result;
  }

  public async Task<bool> Create(CreateCustomerRequest request)
  {
    await using var context = DatabaseHelper.GetDbContext();
    if (context == null) return false;
    try
    {
      var existedCustomer = await context.Customers
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Email == request.Email);

      if (existedCustomer != null) return false;

      var newCustomer = new Customer(request.FullName, request.DayOfBirth, request.Email);
      await context.AddAsync(newCustomer);
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