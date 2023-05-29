using Microsoft.AspNetCore.Mvc;
using WebAPIApplication.Models.Requests.Order;
using WebAPIApplication.Services;

namespace WebAPIApplication.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
  private readonly IOrderService _ordertService;
  public OrderController(IOrderService orderService)
  {
    _ordertService = orderService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    try
    {
      var response = await _ordertService.GetAll();
      return Ok(response);
    }
    catch
    {
      return BadRequest();
    }

  }

  [HttpGet("check-valid-data")]
  public async Task<IActionResult> CheckValidData()
  {
    try
    {
      var response = await _ordertService.CheckValidData();
      return Ok(response);
    }
    catch
    {
      return BadRequest();
    }

  }

  [HttpPost]
  public async Task<IActionResult> Add([FromBody] CreateOrderRequest request)
  {
    try
    {
      var result = await _ordertService.Add(request);

      if (result != true) return BadRequest();

      return NoContent();
    }
    catch
    {
      return BadRequest();
    }
  }
}