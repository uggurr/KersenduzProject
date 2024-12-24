using kersenduz.Shared.Models;
using kersenduz.WebApi.Service.Interface;

namespace kersenduz.WebApi.Service.Implement
{
  public class ProductService : IProductService
  {
    public async Task<List<Product>> ListProductsAsyncService()
    {
      List<Product> products = new List<Product>();
      for (int i = 1; i < 10; i++)
      {
        var product = new Product();
        product.Id = i;
        product.CreatedDate = DateTime.Now;
        product.ModifiedDate = DateTime.Now;
        product.IsActive = true;
        product.Name = "Test" + i;
        product.CategoryId = 1;
        product.Img = "Test" + i;
        product.CostPrice = 100 + i;
        products.Add(product);
      }
      return products;
    }
  }
}
