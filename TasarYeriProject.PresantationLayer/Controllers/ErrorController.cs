using Microsoft.AspNetCore.Mvc;

namespace TasarYeriProject.PresantationLayer.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error404(int code)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConfirmMailError()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmMailError(int id)
        {
            return RedirectToAction("Index","ConfirmMail");
        }

        [HttpGet]
        public IActionResult StatusError()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StatusError(int id)
        {
            return RedirectToAction("Index", "Login");
        }
    }
}
