using Microsoft.AspNetCore.Mvc;

namespace kersenduz.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
