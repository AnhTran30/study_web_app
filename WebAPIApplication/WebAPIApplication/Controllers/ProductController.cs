using Microsoft.AspNetCore.Mvc;
using WebAPIApplication.Models.Requests.Product;
using WebAPIApplication.Services;

namespace WebAPIApplication.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
  private readonly IProductService _productService;
  public ProductController(IProductService productService)
  {
    _productService = productService;
  }

  [HttpGet]
  public async Task<IActionResult> GetProduct([FromQuery] Guid? shopId)
  {
    try
    {
      var products = await _productService.GetProducts(shopId);
      return Ok(products);
    }
    catch
    {
      return BadRequest();
    }

  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
  {
    try
    {
      var result = await _productService.Create(request);

      if (result != true) return BadRequest();

      return NoContent();
    }
    catch
    {
      return BadRequest();
    }
  }
}