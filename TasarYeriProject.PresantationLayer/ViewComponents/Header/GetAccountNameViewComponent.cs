using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.ViewComponents.Header
{
	public class GetAccountNameViewComponent:ViewComponent
	{
		private readonly UserManager<AppUser> _userManager;
		public GetAccountNameViewComponent(UserManager<AppUser> userManager)
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
				ViewBag.ImageUrl = user.ImageUrl;
			}

			return View();
		}
	}
}
