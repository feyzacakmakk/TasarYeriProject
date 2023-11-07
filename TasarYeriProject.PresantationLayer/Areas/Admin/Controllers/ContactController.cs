using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;

namespace TasarYeriProject.PresantationLayer.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ContactController : Controller
	{
		ContactManager contactManager=new ContactManager(new EfContactRepository());
		public IActionResult Index()
		{
			var result=contactManager.TGetList();
			return View(result);
		}
	}
}
