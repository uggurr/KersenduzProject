using kersenduz.Shared.Models;

namespace kersenduz.WebApi.Service.Interface;

public interface ILoginRegisterService
{
    Task<User> RegisterService(User model);
}
