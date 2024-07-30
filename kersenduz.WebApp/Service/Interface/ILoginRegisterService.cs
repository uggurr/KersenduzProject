using kersenduz.Shared.Models;

namespace kersenduz.WebApp.Service.Interface
{
    public interface ILoginRegisterService
    {
        Task<User> RegisterService(User model);
    }
}
