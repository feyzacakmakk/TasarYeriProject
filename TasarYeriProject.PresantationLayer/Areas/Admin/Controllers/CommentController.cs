using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;
using X.PagedList;

namespace TasarYeriProject.PresantationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CommentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        CommentManager commentManager = new CommentManager(new EfCommentRepository());

        public CommentController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("/Admin/Comment/AdminCommentList/")]
        public IActionResult AdminCommentList(int page=1)
        {
            var result = commentManager.TGetCommentListWithUserName().ToPagedList(page, 5);
            return View(result);
        }

        [HttpGet("/Admin/Comment/AdminDeleteComment/{comment_id}/")]
        public IActionResult AdminDeleteComment(int comment_id)
        {
            var result = commentManager.TGetByID(comment_id);
            if (result != null)
            {
                commentManager.TDelete(result);
                return RedirectToAction("AdminCommentList");
            }
            return View();
        }
    }
}
