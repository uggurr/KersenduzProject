using kersenduz.Core.Repository.Interface;
using kersenduz.DataAccess.UnitOfWork.Interface;
using kersenduz.Shared.Models;
using kersenduz.WebApi.Service.Interface;

namespace kersenduz.WebApi.Service.Implement
{
  public class RawMaterialService : IRawMaterialService
  {
    private readonly IRawMaterialRepository _rawMaterialRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RawMaterialService(IRawMaterialRepository rawMaterialRepository, IUnitOfWork unitOfWork)
    {
      _rawMaterialRepository = rawMaterialRepository;
      _unitOfWork = unitOfWork;
    }

    public async Task<RawMaterial> AddRawMaterialAsync(RawMaterial rawMaterial)
    {
      rawMaterial.CreatedDate = DateTime.Now;
      rawMaterial.ModifiedDate = DateTime.Now;
      rawMaterial.IsActive = true;
      var response = await _rawMaterialRepository.AddAsync(rawMaterial);
      await _unitOfWork.SaveChangeAsync();
      return response;
    }

    public async Task<List<RawMaterial>> ListRawMaterialsAsync()
    {
      var response = await _rawMaterialRepository.ListAsync(x => x.IsActive == true);
      return response;
    }

    public async Task<RawMaterial> UpdateRawMaterial(RawMaterial rawMaterial)
    {
      var result = _rawMaterialRepository.GetWhere(x => x.IsActive && x.Id == rawMaterial.Id).FirstOrDefault();
      result.Name = rawMaterial.Name;
      result.Price = rawMaterial.Price;
      result.UnitId = rawMaterial.UnitId;
      result.ModifiedDate = DateTime.Now;
      var response =  _rawMaterialRepository.UpdateAsync(result);
      await _unitOfWork.SaveChangeAsync();
      return response;
    }
  }
}
