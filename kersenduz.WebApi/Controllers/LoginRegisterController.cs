using kersenduz.Shared.Models;
using kersenduz.WebApi.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace kersenduz.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginRegisterController : Controller
{
    private readonly ILoginRegisterService _loginRegisterService;

    public LoginRegisterController(ILoginRegisterService loginRegisterService)
    {
        _loginRegisterService = loginRegisterService;
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register(User model)
    {
        try
        {
            var result = await _loginRegisterService.RegisterService(model);
            return Ok(result);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }
}
