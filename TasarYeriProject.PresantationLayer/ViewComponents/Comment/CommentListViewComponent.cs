using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.ViewComponents.Comment
{
    public class CommentListViewComponent:ViewComponent
    {
        CommentManager commentManager=new CommentManager(new EfCommentRepository());
        private readonly UserManager<AppUser> _userManager;
        public CommentListViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int product_id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            if (user != null)
            {
                ViewBag.user_id = user.Id;
            }
            var values=commentManager.TGetCommentsWithUserName(product_id);
            var count=values.Count();
            ViewBag.Count=count;
            
            return View(values);
        }
    }
}
