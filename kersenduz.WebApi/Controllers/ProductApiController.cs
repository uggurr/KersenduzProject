using kersenduz.WebApi.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace kersenduz.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductApiController:Controller
{

  private readonly IProductService _productService;

  public ProductApiController(IProductService productService)
  {
    _productService = productService;
  }

  [HttpGet("ListProductAsync")]
  public async Task<IActionResult> ListProductAsync()
  {
    var result = await _productService.ListProductsAsyncService();
    return Ok(result);
  }
}
