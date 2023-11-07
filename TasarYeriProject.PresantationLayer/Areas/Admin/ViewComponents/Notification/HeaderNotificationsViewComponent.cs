using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.Areas.Admin.ViewComponents.Notification
{
    public class HeaderNotificationsViewComponent:ViewComponent
    {
        NotificationManager notificationManager = new NotificationManager(new EfNotificationRepository());
        private readonly UserManager<AppUser> _userManager;
        public HeaderNotificationsViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var user_id = Convert.ToInt32(user.Id);
            var values = notificationManager.TGetNotificationsByUserId(user_id);
            var count = values.Count();
            ViewBag.Count = count;
            return View(values);
        }
    }
}
