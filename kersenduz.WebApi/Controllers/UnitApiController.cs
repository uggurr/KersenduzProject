using kersenduz.WebApi.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace kersenduz.WebApi.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class UnitApiController: Controller
  {
    private readonly IUnitApiService _unitApiService;

    public UnitApiController(IUnitApiService unitApiService)
    {
      _unitApiService = unitApiService;
    }

    [HttpGet("ListUnitsAsync")]
    public async Task<IActionResult> ListUnitsAsync()
    {
      var result = await _unitApiService.ListUnitsServiceAsync();
      return Ok(result);
    }

    [HttpGet("GetUnitById/{id}")]
    public IActionResult GetUnitById(int id)
    {
      var result = _unitApiService.GetUnitById(id);
      return Ok(result);
    }
  }
}
