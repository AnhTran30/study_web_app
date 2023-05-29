using Microsoft.AspNetCore.Mvc;
using WebAPIApplication.Models.Requests.Shop;
using WebAPIApplication.Services;

namespace WebAPIApplication.Controllers;

[ApiController]
[Route("api/shops")]
public class ShopController : ControllerBase
{
  private readonly IShopService _shopService;
  public ShopController(IShopService shopService)
  {
    _shopService = shopService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    try
    {
      var shops = await _shopService.GetAll();
      return Ok(shops);
    }
    catch
    {
      return BadRequest();
    }
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateShopRequest request)
  {
    try
    {
      var result = await _shopService.Create(request);

      if (result != true) return BadRequest();

      return NoContent();
    }
    catch
    {
      return BadRequest();
    }
  }
}