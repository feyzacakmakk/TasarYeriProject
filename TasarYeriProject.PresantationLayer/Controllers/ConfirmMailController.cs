using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.EntityLayer.Concrete;
using TasarYeriProject.PresantationLayer.Models;

namespace TasarYeriProject.PresantationLayer.Controllers
{
	public class ConfirmMailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = TempData["Mail"];
            ViewBag.value=value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
        {
            var user = await _userManager.FindByEmailAsync(confirmMailViewModel.Mail);
            if (user.ConfirmCode == confirmMailViewModel.ConfirmCode) 
            {
                user.EmailConfirmed = true; //doğrulandı işlemini db ye gönderiyoruz
                await _userManager.UpdateAsync(user); //kullanıcıyı db de güncelledik
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
