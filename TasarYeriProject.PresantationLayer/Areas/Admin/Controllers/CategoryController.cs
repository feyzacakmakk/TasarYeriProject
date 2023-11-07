using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;
using X.PagedList;

namespace TasarYeriProject.PresantationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CategoryController : Controller
    {
        public CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());

        [HttpGet("/Admin/Category/CategoryList/")]
        public IActionResult CategoryList(int page=1)
        {
            var values = categoryManager.TGetList().ToPagedList(page, 5);
            return View(values);

        }

        [HttpGet]
        public IActionResult AdminAddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminAddCategory(Category category)
        {
            categoryManager.TInsert(category);
            return RedirectToAction("CategoryList");
        }

        [HttpGet("/Admin/Category/AdminEditCategory/{category_id}/")]
        public IActionResult AdminEditCategory(int category_id)
        {
            var result = categoryManager.TGetByID(category_id);
            return View(result);
        }

        [HttpPost]
        public IActionResult AdminEditCategory(Category category)
        {
            categoryManager.TUpdate(category);
            return RedirectToAction("CategoryList");
        }

        [HttpGet("/Admin/Category/AdminDeleteCategory/{category_id}/")]
        public IActionResult AdminDeleteCategory(int category_id)
        {
            var result = categoryManager.TGetByID(category_id);
            categoryManager.TDelete(result);
            return RedirectToAction("CategoryList");
        }
    }
}
