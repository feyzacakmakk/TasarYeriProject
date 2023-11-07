using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.Controllers
{
    public class NotificationController : Controller
    {
        NotificationManager _notificationManager=new NotificationManager(new EfNotificationRepository());
        private readonly UserManager<AppUser> _userManager;

        public NotificationController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllNotification()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var user_id = Convert.ToInt32(user.Id);
            var result=_notificationManager.TGetNotificationsByUserId(user_id);
            return View(result);
        }
    }
}
