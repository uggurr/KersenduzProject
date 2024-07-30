using kersenduz.Core.Repository.Interface;
using kersenduz.DataAccess.UnitOfWork.Interface;
using kersenduz.Shared.Models;
using kersenduz.WebApi.Service.Interface;

namespace kersenduz.WebApi.Service.Implement;

public class LoginRegisterService : ILoginRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoginRegisterService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> RegisterService(User model)
    {
        try
        {
            var result = _userRepository.AddAsync(model);
            await _unitOfWork.SaveChangeAsync();
            return result.Result;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
