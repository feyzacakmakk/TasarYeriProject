using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.ViewComponents.SellerDashboard
{
    public class UserProductsListViewComponent:ViewComponent
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly ProductManager _productManager=new ProductManager(new EfProductRepository());
		public UserProductsListViewComponent(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
            DateTime TodaysDate = DateTime.Today;
            var user = await _userManager.GetUserAsync(HttpContext.User);
			var user_id=Convert.ToInt32(user.Id);
			var result = _productManager.TGetProductsWithProductOwnerToday(user_id);
			var productAddedToday = result.Count(x => x.AddedDate == TodaysDate);
			ViewBag.ProductAddedToday = productAddedToday;
            return View(result);
		}
	}
}
