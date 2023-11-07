using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.DtoLayer.Dtos.AppUserDtos;
using TasarYeriProject.EntityLayer.Concrete;
using X.PagedList;

namespace TasarYeriProject.PresantationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        public AppUserManager appUserManager = new AppUserManager(new EfAppUserRespository());
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(int page=1)
        {
            var result = appUserManager.TGetList().ToPagedList(page,4);
            return View(result);
        }

        [HttpGet("/Admin/User/UserDetail/{user_id}/")]
        public async Task<IActionResult> UserDetail(int user_id)
        {
            var user = await _userManager.FindByIdAsync(user_id.ToString());
            TempData["userId"] = user_id;
            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamazsa 404 hatası döndürün veya başka bir işlem yapın.
            }

            var appUserEditDto = new AppUserEditDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                ImageUrl = user.ImageUrl,
                CityName = user.CityName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AboutText = user.AboutText
            };

            return View(appUserEditDto);
        }



        [HttpPost]
        public async Task<IActionResult> UserDetail(AppUserEditDto appUserEditDto)
        {

                var user_id = TempData["userId"];
                var user = await _userManager.FindByIdAsync(user_id.ToString());
                user.Name = appUserEditDto.Name;
                user.Surname = appUserEditDto.Surname;
                user.Address = appUserEditDto.Address;
                user.ImageUrl = appUserEditDto.ImageUrl;
                user.CityName = appUserEditDto.CityName;
                user.Email = appUserEditDto.Email;
                user.PhoneNumber = appUserEditDto.PhoneNumber;
                user.AboutText = appUserEditDto.AboutText;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", "User",new {user_id});
                }

            return View();
        }

        [HttpGet("/Admin/User/DeleteUser/")]
        public async Task<IActionResult> DeleteUser()
        {
            var user_id = (int)TempData["userId"];
            var user = await _userManager.FindByIdAsync(user_id.ToString());
            appUserManager.TDeleteUserWithMessage(user_id);
            appUserManager.TDelete(user);
            return RedirectToAction("Index");
        }

        [HttpGet("/Admin/User/GetUserPassive/")]
        public async Task<IActionResult> GetUserPassive()
        {
            var user_id = (int)TempData["userId"];
            var user = await _userManager.FindByIdAsync(user_id.ToString());
            if (user.UserStatus == true)
            {
                user.UserStatus = false;
                appUserManager.TUpdate(user);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("/Admin/User/GetUserActive/")]
        public async Task<IActionResult> GetUserActive()
        {
            var user_id = (int)TempData["userId"];
            var user = await _userManager.FindByIdAsync(user_id.ToString());
            if (user.UserStatus == false)
            {
                user.UserStatus = true;
                appUserManager.TUpdate(user);
            }
            return RedirectToAction("Index");
        }
    }
}
