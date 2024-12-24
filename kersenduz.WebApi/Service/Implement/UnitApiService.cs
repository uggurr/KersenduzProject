using kersenduz.Core.Repository.Interface;
using kersenduz.Shared.Models;
using kersenduz.WebApi.Service.Interface;

namespace kersenduz.WebApi.Service.Implement
{
  public class UnitApiService : IUnitApiService
  {
    private readonly IUnitRepository _unitRepository;

    public UnitApiService(IUnitRepository unitRepository)
    {
      _unitRepository = unitRepository;
    }

    public Unit GetUnitById(int id)
    {
      var response = _unitRepository.GetWhere(x => x.Id == id && x.IsActive == true).FirstOrDefault();
      return response;
    }

    public async Task<List<Unit>> ListUnitsServiceAsync()
    {
      var response = await _unitRepository.ListAsync(x=> x.IsActive == true);
      return response.ToList();
    }
  }
}
