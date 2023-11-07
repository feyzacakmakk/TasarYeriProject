using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.ViewComponents.Comment
{
    public class CommentListDashboardViewComponent:ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        private readonly UserManager<AppUser> _userManager;
        public CommentListDashboardViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var user_id=Convert.ToInt32(user.Id);
            if (user != null)
            {
                ViewBag.user_id = user_id;
            }
            var values = commentManager.TGetCommentsForUserProducts(user_id);
            var count = values.Count();
            ViewBag.Count = count;

            return View(values);
        }
    }
}
