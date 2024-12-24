using kersenduz.Shared.Models;

namespace kersenduz.WebApi.Service.Interface
{
  public interface IProductService
  {
    Task<List<Product>> ListProductsAsyncService();
  }
}
