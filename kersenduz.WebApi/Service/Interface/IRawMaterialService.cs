using kersenduz.Shared.Models;

namespace kersenduz.WebApi.Service.Interface
{
  public interface IRawMaterialService
  {
    Task<RawMaterial> AddRawMaterialAsync(RawMaterial rawMaterial);
    Task<List<RawMaterial>> ListRawMaterialsAsync();
    Task<RawMaterial> UpdateRawMaterial(RawMaterial rawMaterial);
  }
}
