using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.Controllers
{
	public class CommentController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        CommentManager commentManager =new CommentManager(new EfCommentRepository());

        public CommentController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var result = commentManager.TGetCommentListWithUserName();
            return View(result);
        }

        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            comment.CommentDate = Convert.ToDateTime(DateTime.UtcNow.AddHours(3));
            //comment.CommentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            comment.CommentStatus = true;
            comment.AppUserId = user.Id;
            ViewBag.UserFullName = $"{user.Name} {user.Surname}";
            commentManager.TInsert(comment);
            return RedirectToAction("ProductDetail", "Product", new { product_id = comment.ProductId });

        }


        [HttpGet("/Comment/DeleteComment/{comment_id}/{product_id}/")]
        public IActionResult DeleteComment(int comment_id, int product_id) 
        {
            var result = commentManager.TGetByID(comment_id);
            if (result != null)
            {
                commentManager.TDelete(result);
                return RedirectToAction("ProductDetail", "Product", new {  product_id });
            }
            return View();
        }

        [HttpGet("/Comment/SellerDeleteComment/{comment_id}/{product_id}/")]
        public IActionResult SellerDeleteComment(int comment_id, int product_id)
        {
            var result = commentManager.TGetByID(comment_id);
            if (result != null)
            {
                commentManager.TDelete(result);
                return RedirectToAction("SellerDashboardProductDetail", "Product", new { product_id });
            }
            return View();
        }

        [HttpGet("/Comment/SellerDeleteComment/{comment_id}/")]
        public IActionResult SellerDeleteComment(int comment_id)
        {
            var result = commentManager.TGetByID(comment_id);
            if (result != null)
            {
                commentManager.TDelete(result);
                return RedirectToAction("Index", "SellerDashboard");
            }
            return View();
        }

    }
}
