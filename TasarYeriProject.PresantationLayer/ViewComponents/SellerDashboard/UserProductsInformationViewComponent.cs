using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.DataAccessLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.ViewComponents.SellerDashboard
{
    public class UserProductsInformationViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var context = new Context();
            var values = context.Products.ToList();
            return View(values);
        }
    }
}
