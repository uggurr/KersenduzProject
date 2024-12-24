using kersenduz.Shared.Models;
using kersenduz.WebApi.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace kersenduz.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RawMaterialApiController:Controller
  {
    private readonly IRawMaterialService _materialService;

    public RawMaterialApiController(IRawMaterialService materialService)
    {
      _materialService = materialService;
    }

    [HttpPost("AddRawMaterialAsync")]
    public async Task<IActionResult> AddRawMaterialAsync(RawMaterial rawMaterial)
    {
      var response = await _materialService.AddRawMaterialAsync(rawMaterial);
      return Ok(response);
    }

    [HttpGet("ListRawMaterialAsync")]
    public async Task<IActionResult> ListRawMaterialAsync()
    {
      var response = await _materialService.ListRawMaterialsAsync();
      return Ok(response);
    }

    [HttpPost("UpdateRawMaterial")]
    public IActionResult UpdateRawMaterial(RawMaterial rawMaterial)
    {
      var response = _materialService.UpdateRawMaterial(rawMaterial);
      return Ok(response);
    }
  }
}
