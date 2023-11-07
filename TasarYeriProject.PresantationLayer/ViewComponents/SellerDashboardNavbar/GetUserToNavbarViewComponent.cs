using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.ViewComponents.Navbar
{
	
	public class GetUserToNavbarViewComponent:ViewComponent
	{
		private readonly UserManager<AppUser> _userManager;
		public GetUserToNavbarViewComponent(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);

			if (user != null)
			{
				ViewBag.UserFullName = $"{user.Name} {user.Surname}";
				ViewBag.UserEmail = user.Email;
				ViewBag.ImageUrl=user.ImageUrl;
			}

			return View();
		}
	}
}
