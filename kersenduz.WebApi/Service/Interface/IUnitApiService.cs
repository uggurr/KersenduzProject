using kersenduz.Shared.Models;

namespace kersenduz.WebApi.Service.Interface
{
  public interface IUnitApiService
  {
    Task<List<Unit>> ListUnitsServiceAsync();
    Unit GetUnitById(int id);
  }
}
