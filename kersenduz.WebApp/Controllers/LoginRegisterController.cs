using kersenduz.Shared.Models;
using kersenduz.WebApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace kersenduz.WebApp.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly ILoginRegisterService _loginRegisterService;

        public LoginRegisterController(ILoginRegisterService loginRegisterService)
        {
            _loginRegisterService = loginRegisterService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<User> Register([FromBody]User model)
        {
            return await _loginRegisterService.RegisterService(model);
        }
    }
}
