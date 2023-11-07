using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;

namespace TasarYeriProject.PresantationLayer.ViewComponents.Category
{
    public class CategoryListViewComponent : ViewComponent
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        public IViewComponentResult Invoke()
        {
            var values = categoryManager.TGetCategories();
            return View(values);
        }
    }
}
