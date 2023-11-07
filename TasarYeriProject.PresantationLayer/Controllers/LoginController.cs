using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.ValidationRules.AppUserValidationRules;
using TasarYeriProject.BusinessLayer.ValidationRules.ProductValidationRules;
using TasarYeriProject.DtoLayer.Dtos.AppUserDtos;
using TasarYeriProject.DtoLayer.Dtos.ProductDtos;
using TasarYeriProject.EntityLayer.Concrete;
using TasarYeriProject.PresantationLayer.Models;

namespace TasarYeriProject.PresantationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        
        [HttpPost]
        public async Task<IActionResult> Index(AppUserLoginDto appUserLoginDto)
        {
            //AppUserLoginValidator appuserLoginValidator = new AppUserLoginValidator();
            //ValidationResult result = appuserLoginValidator.Validate(appUserLoginDto);
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(appUserLoginDto.Email);
                var result2 = await _signInManager.PasswordSignInAsync(user.UserName, appUserLoginDto.Password, false, true);
                bool isConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                bool isStatus = user.UserStatus;
                if (result2.Succeeded )
                {
                    if (isConfirmed)
                    {
                        if (isStatus)
                        {

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("StatusError", "Error");
                        }
                    }
                    else
                    {
                        return RedirectToAction("ConfirmMailError", "Error");
                    }
                    

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email veya şifre hatalı.");
                    
                }
            }
            // Eğer kullanıcı yoksa veya şifre yanlışsa, uyarı mesajını gösterir
            
            return View(appUserLoginDto);
                    
        }

        [HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
