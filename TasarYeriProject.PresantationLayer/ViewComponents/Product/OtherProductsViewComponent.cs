using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.DataAccessLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.ViewComponents.Product
{
    public class OtherProductsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var context = new Context();
            var allProducts = context.Products.ToList();
            var random = new Random();
            var result = allProducts.OrderBy(x => random.Next()).ToList();
            //ürünleri random şekilde alıp listeye çeviriyor
            return View(result);
        }
    }
}
