using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.Controllers
{
	[Authorize(Roles = "Satıcı")]
	public class CategoryController : Controller
	{
		public CategoryManager categoryManager=new CategoryManager(new EfCategoryRepository());
		public IActionResult Index()
		{
            var values = categoryManager.TGetList();
            return View(values);
            
		}

		[HttpGet]
		public IActionResult AddCategory()
		{
            return View();
        }

		[HttpPost]
		public IActionResult AddCategory(Category category)
		{
			categoryManager.TInsert(category);
			return RedirectToAction("Index");
		}

        [HttpGet("/Category/EditCategory/{category_id}/")]
        public IActionResult EditCategory(int category_id)
        {
            var result=categoryManager.TGetByID(category_id);
            return View(result);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            categoryManager.TUpdate(category);
            return RedirectToAction("Index");
        }

        [HttpGet("/Category/DeleteCategory/{category_id}/")]
        public IActionResult DeleteCategory(int category_id)
        {
            var result = categoryManager.TGetByID(category_id);
            categoryManager.TDelete(result);
            return RedirectToAction("Index");
        }
    }

}
