using kersenduz.Shared.Models;
using kersenduz.Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace kersenduz.Web_App.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController: Controller
  {
    private readonly HttpClient _httpClient;

    public ProductsController(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    #region Wiew Metod
    [Route("ProductsPage")]
    public IActionResult ProductsPage()
    {
      return View("ProductPage");
    }

    [Route("AddProductPage")]
    public IActionResult AddProductPage()
    {
      return View();
    }

    [Route("UpdateProductPage/{id}")]
    public IActionResult UpdateProductPage(int id)
    {
      // ID'yi alarak gerekli işlemleri yapabilirsiniz
      return View(id); // Görünüm dosyasına ID'yi model olarak gönderiyoruz
    }
    #endregion


    #region ActionMetot

    [HttpGet("ListProductAsync")]
    public async Task<IActionResult> ListProductAsync()
    {
      var response = await _httpClient.GetAsync("https://localhost:44300/api/ProductApi/ListProductAsync");

      // API'den dönen cevap başarılıysa
      if (response.IsSuccessStatusCode)
      {
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var products = JsonConvert.DeserializeObject<List<Product>>(jsonResponse);

        return Ok(products);
      }
      else
      { 
        return StatusCode((int)response.StatusCode, "API'den veri alınırken bir hata oluştu.");
      }
    }



    [HttpGet("GetProductDetails/{id}")]
    public IActionResult GetProductDetails(int id)
    {
      // Veritabanından ürün bilgilerini alın (örnek bir veri)
      var productDetail = new ProductDetailVM();
      productDetail.Id = id;
      productDetail.ProductName = "Test" + id;
      productDetail.ProductCategory = "Ekmek";
      productDetail.ProductPrice = 100 + id;
      productDetail.RawMetarials = new List<RawMetarials>
      {
                    new RawMetarials { Id= 1, Name = "Un", Unit = "Kg",Quantity = 10 },
                    new RawMetarials { Id= 2, Name = "Yağ",Unit = "L", Quantity = 100 },
                    new RawMetarials { Id=3, Name = "Tuz", Unit = "Gr",Quantity = 0.10 }
      };

      return Ok(productDetail); // JSON formatında döndür
    }
    #endregion
  }
}
