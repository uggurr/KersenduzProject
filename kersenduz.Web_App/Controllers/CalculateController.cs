using Microsoft.AspNetCore.Mvc;

namespace kersenduz.Web_App.Controllers
{
  public class CalculateController:Controller
  {

    public IActionResult CalculatePage()
    {
      return View();
    }
  }
}
