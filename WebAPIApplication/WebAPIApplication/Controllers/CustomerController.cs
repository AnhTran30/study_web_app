using Microsoft.AspNetCore.Mvc;
using WebAPIApplication.Models.Requests.Customer;
using WebAPIApplication.Services;

namespace WebAPIApplication.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
  private readonly ICustomerService _customerService;
  public CustomerController(ICustomerService customerService)
  {
    _customerService = customerService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    try
    {
      var customers = await _customerService.GetAll();
      return Ok(customers);
    }
    catch
    {
      return BadRequest();
    }

  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
  {
    try
    {
      var result = await _customerService.Create(request);

      if (result != true) return BadRequest();

      return NoContent();
    }
    catch
    {
      return BadRequest();
    }
  }
}