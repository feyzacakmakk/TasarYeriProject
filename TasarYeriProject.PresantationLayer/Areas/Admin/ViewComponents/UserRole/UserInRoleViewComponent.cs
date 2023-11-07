using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasarYeriProject.EntityLayer.Concrete;
using TasarYeriProject.PresantationLayer.Areas.Admin.Models;

namespace TasarYeriProject.PresantationLayer.Areas.Admin.ViewComponents.UserRole
{
    public class UserInRoleViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public UserInRoleViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int user_id)
        {
            var user = await _userManager.FindByIdAsync(user_id.ToString());

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var model = new UserRoleViewModel
                {
                    UserName = user.UserName,
                    Roles = roles
                };

                return View(model);
            }

           
            return View(new UserRoleViewModel());
        }
    }
}
