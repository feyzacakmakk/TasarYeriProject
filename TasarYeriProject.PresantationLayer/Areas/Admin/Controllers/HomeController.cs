using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.DtoLayer.Dtos.AppUserDtos;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(value);
        }


        [HttpGet]
        public async Task<IActionResult> ProfileDetail()
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
        public async Task<IActionResult> ProfileDetail(AppUserEditDto appUserEditDto)
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
                    return RedirectToAction("ProfileDetail", "Home");
                }
            }

            return View();
        }
    }
}
