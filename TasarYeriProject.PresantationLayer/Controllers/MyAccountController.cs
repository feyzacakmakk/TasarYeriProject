using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.DtoLayer.Dtos.AppUserDtos;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.Controllers
{
    [Authorize]
    public class MyAccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public MyAccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			AppUserEditDto appUserEditDto = new AppUserEditDto();
			appUserEditDto.Name = values.Name;
			appUserEditDto.Surname = values.Surname;
			appUserEditDto.Address = values.Address;
			appUserEditDto.ImageUrl = values.ImageUrl;
			appUserEditDto.CityName = values.CityName;
			appUserEditDto.Email = values.Email;
			appUserEditDto.PhoneNumber = values.PhoneNumber;
			appUserEditDto.AboutText = values.AboutText;
			return View(appUserEditDto);
		}


		[HttpPost]
		public async Task<IActionResult> Index(AppUserEditDto appUserEditDto)
		{
			if (appUserEditDto.Password == appUserEditDto.ConfirmPassword)
			{
				var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (appUserEditDto.Image != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(appUserEditDto.Image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/userimages/" + imageName;
                    //veritabanına buna göre kayıt işlemi gerçekleştirilecek
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    await appUserEditDto.Image.CopyToAsync(stream);
                    user.ImageUrl = "/userimages/" + imageName;
                }

                user.Name = appUserEditDto.Name;
				user.Surname = appUserEditDto.Surname;
				user.Address = appUserEditDto.Address;
				//user.ImageUrl = appUserEditDto.ImageUrl;
				user.CityName = appUserEditDto.CityName;
				user.Email = appUserEditDto.Email;
				user.PhoneNumber = appUserEditDto.PhoneNumber;
				user.AboutText = appUserEditDto.AboutText;
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEditDto.Password);
				var result = await _userManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
			}

			return View();
		}

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserEditDto appUserEditDto = new AppUserEditDto();
            appUserEditDto.Name = values.Name;
            appUserEditDto.Surname = values.Surname;
            appUserEditDto.Address = values.Address;
            appUserEditDto.ImageUrl = values.ImageUrl;
			ViewBag.Image = values.ImageUrl;
            appUserEditDto.CityName = values.CityName;
            appUserEditDto.Email = values.Email;
            appUserEditDto.PhoneNumber = values.PhoneNumber;
            appUserEditDto.AboutText = values.AboutText;
            return View(appUserEditDto);
        }


        [HttpPost]
        public async Task<IActionResult> EditProfile(AppUserEditDto appUserEditDto)
        {
			if (string.IsNullOrEmpty(appUserEditDto.Password) || string.IsNullOrEmpty(appUserEditDto.ConfirmPassword))
			{
				ModelState.AddModelError("", "Lütfen şifrenizi girin.");
				return View(appUserEditDto); // Hata mesajı ile birlikte sayfayı tekrar göster
			}
            else
            {
				if (appUserEditDto.Password == appUserEditDto.ConfirmPassword)
				{
					var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    
                    if (appUserEditDto.Image != null)
					{
						var resource = Directory.GetCurrentDirectory();
						var extension = Path.GetExtension(appUserEditDto.Image.FileName);
						var imageName = Guid.NewGuid() + extension;
						var saveLocation = resource + "/wwwroot/userimages/" + imageName;
						//veritabanına buna göre kayıt işlemi gerçekleştirilecek
						var stream = new FileStream(saveLocation, FileMode.Create);
						await appUserEditDto.Image.CopyToAsync(stream);
						user.ImageUrl = "/userimages/" + imageName;
					}

					user.Name = appUserEditDto.Name;
					user.Surname = appUserEditDto.Surname;
					user.Address = appUserEditDto.Address;
					//user.ImageUrl = appUserEditDto.ImageUrl;
					user.CityName = appUserEditDto.CityName;
					user.Email = appUserEditDto.Email;
					user.PhoneNumber = appUserEditDto.PhoneNumber;
					user.AboutText = appUserEditDto.AboutText;
					user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEditDto.Password);
					var result = await _userManager.UpdateAsync(user);
					if (result.Succeeded)
					{
						return RedirectToAction("Index");
					}
				}
				else
				{
					ModelState.AddModelError("", "Girdiğiniz şifreler aynı değil.");
					return View(appUserEditDto); // Hata mesajı ile birlikte sayfayı tekrar göster
				}
			}
			

            return View();
        }

        public IActionResult OrderInfo()
		{
			return View();
		}

		public IActionResult Favorites()
		{
			return View();
		}



	}
}
