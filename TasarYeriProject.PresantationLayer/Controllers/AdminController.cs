using Microsoft.AspNetCore.Mvc;

namespace TasarYeriProject.PresantationLayer.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult deneme()
        {
            return View();
        }
    }
}
