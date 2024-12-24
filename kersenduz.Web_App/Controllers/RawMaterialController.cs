using kersenduz.Shared.Models;
using kersenduz.Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace kersenduz.Web_App.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RawMaterialController:Controller
{
  private readonly HttpClient _httpClient;

  public RawMaterialController(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }
  #region ViewMetod
  [Route("RawMaterialPage")]
  public IActionResult RawMaterialPage()
  {
    return View();
  }
  [Route("AddRawMetarialPage")]
  public IActionResult AddRawMetarialPage()
  {
    return View();
  }
  [Route("UpdateRawMetarialPage")]
  public IActionResult UpdateRawMetarialPage()
  {
    return View();
  }
  #endregion
  #region ActionMetod
  [HttpPost("AddRawMaterialAsync")]
  public async Task<IActionResult> AddRawMaterialAsync(RawMaterial rawMaterial)
  {
    var response = await _httpClient.PostAsJsonAsync("https://localhost:44300/api/RawMaterialApi/AddRawMaterialAsync", rawMaterial);

    // API'den dönen cevap başarılıysa
    if (response.IsSuccessStatusCode)
    {
      var jsonResponse = await response.Content.ReadAsStringAsync();

      var products = JsonConvert.DeserializeObject<RawMaterial>(jsonResponse);
      RawMaterialVm rawMaterialVm = new RawMaterialVm();
      rawMaterialVm.Id = products.Id;
      rawMaterialVm.Name = products.Name;
      rawMaterialVm.UnitId = products.UnitId;
      rawMaterialVm.Price = products.Price;
      rawMaterialVm.ModifietedDate = products.ModifiedDate;
      var unit = await _httpClient.GetAsync($"https://localhost:44300/api/UnitApi/GetUnitById/{products.UnitId}");
      if (response.IsSuccessStatusCode)
      {
        var unitResponse = await unit.Content.ReadAsStringAsync();

        var unitDeserial = JsonConvert.DeserializeObject<Unit>(unitResponse);

        rawMaterialVm.UnitName = unitDeserial.Name;
      }
      return Ok(rawMaterialVm);
    }
    else
    {
      return StatusCode((int)response.StatusCode, "API'den veri alınırken bir hata oluştu.");
    }
  }

  [HttpGet("ListRawMaterialAsync")]
  public async Task<IActionResult> ListRawMaterialAsync()
  {
    var response = await _httpClient.GetAsync("https://localhost:44300/api/RawMaterialApi/ListRawMaterialAsync");
    if (response.IsSuccessStatusCode)
    {
      
      var jsonResponse = await response.Content.ReadAsStringAsync();

      var products = JsonConvert.DeserializeObject<List<RawMaterial>>(jsonResponse);
      List<RawMaterialVm> listRawMaterialVm = new List<RawMaterialVm>();

      foreach (var item in products)
      {
        RawMaterialVm rawMaterialVm = new RawMaterialVm();
        rawMaterialVm.Id = item.Id;
        rawMaterialVm.Name = item.Name;
        rawMaterialVm.UnitId = item.UnitId;
        rawMaterialVm.Price = item.Price;
        rawMaterialVm.ModifietedDate = item.ModifiedDate;
          var unit = await _httpClient.GetAsync($"https://localhost:44300/api/UnitApi/GetUnitById/{item.UnitId}");
        if (response.IsSuccessStatusCode)
        {
          var unitResponse = await unit.Content.ReadAsStringAsync();

          var unitDeserial = JsonConvert.DeserializeObject<Unit>(unitResponse);

          rawMaterialVm.UnitName = unitDeserial.Name;
        }
        listRawMaterialVm.Add(rawMaterialVm);
      }

      return Ok(listRawMaterialVm);
    }
    else
    {
      return StatusCode((int)response.StatusCode, "API'den veri alınırken bir hata oluştu.");
    }
  }

  [HttpPost("UpdateRawMaterialAsync")]
  public async Task<IActionResult> UpdateRawMaterialAsync(RawMaterial rawMaterial)
  {
    var response = await _httpClient.PostAsJsonAsync("https://localhost:44300/api/RawMaterialApi/UpdateRawMaterial", rawMaterial);

    // API'den dönen cevap başarılıysa
    if (response.IsSuccessStatusCode)
    {
      var jsonResponse = await response.Content.ReadAsStringAsync();

      var products = JsonConvert.DeserializeObject<RawMaterial>(jsonResponse);

      return Ok(products);
    }
    else
    {
      return StatusCode((int)response.StatusCode, "API'den veri alınırken bir hata oluştu.");
    }
  }
  #endregion
}
