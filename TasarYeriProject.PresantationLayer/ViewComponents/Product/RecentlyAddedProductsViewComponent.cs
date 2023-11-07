using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.DataAccessLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.ViewComponents.Product
{
    public class RecentlyAddedProductsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var context = new Context();
            var result = context.Products.ToList().TakeLast(5).Reverse();
            //son 5 ürünü listeliyor
            return View(result);
        }

        //ToList().TakeLast(3).Reverse();

    }
}
