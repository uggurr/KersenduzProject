using kersenduz.Shared.Models;
using kersenduz.WebApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace kersenduz.WebApp.Controllers
{
    [Route("[Controller]")]
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

        [Route("Register")]
        [HttpGet]
        public async Task<User> Register([FromBody]User model)
        {
            return await _loginRegisterService.RegisterService(model);
        }
    }
}
