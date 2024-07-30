using kersenduz.Shared.Models;
using kersenduz.WebApp.RequestHelper.Interfaces;
using kersenduz.WebApp.Service.Interface;


namespace kersenduz.WebApp.Service.Implement
{
    public class LoginRegisterService : ILoginRegisterService
    {
        private readonly IRequestHelper _requestHelper;

        public LoginRegisterService(IRequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task<User> RegisterService(User model)
        {
            try
            {
                var result = _requestHelper.Post<User>("https://localhost:44300/api/LoginRegister/Register",model).Result;
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
