using kersenduz.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace kersenduz.Web_App.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UnitController:Controller
  {
    private readonly HttpClient _httpClient;

    public UnitController(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    [HttpGet("ListUnitsAsync")]
    public async Task<IActionResult> ListUnitsAsync()
    {
      var response = await _httpClient.GetAsync("https://localhost:44300/api/UnitApi/ListUnitsAsync");
      if (response.IsSuccessStatusCode)
      {
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var products = JsonConvert.DeserializeObject<List<Unit>>(jsonResponse);

        return Ok(products);
      }
      else
      {
        return StatusCode((int)response.StatusCode, "API'den veri alınırken bir hata oluştu.");
      }
    }

    [HttpGet("GetUnitByIdAsync/{id}")]
    public async Task<IActionResult> GetUnitByIdAsync(int id)
    {
      var response = await _httpClient.GetAsync($"https://localhost:44300/api/UnitApi/GetUnitById/{id}");
      if (response.IsSuccessStatusCode)
      {
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var products = JsonConvert.DeserializeObject<Unit>(jsonResponse);

        return Ok(products);
      }
      else
      {
        return StatusCode((int)response.StatusCode, "API'den veri alınırken bir hata oluştu.");
      }
    }
  }
}
