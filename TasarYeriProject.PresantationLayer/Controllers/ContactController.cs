using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.Controllers
{
	public class ContactController : Controller
    {
        ContactManager contactManager=new ContactManager(new EfContactRepository());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Index(Contact contact)
		{
            contact.ContactDate=DateTime.Parse(DateTime.Now.ToShortDateString());
            contact.ContactStatus = true;
			contactManager.TInsert(contact);
			return RedirectToAction("Index","Contact");
		}
	}
}
