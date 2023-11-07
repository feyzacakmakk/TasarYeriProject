using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class MessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageRepository());

        private readonly UserManager<AppUser> _userManager;

        public MessageController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> AdminInbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var user_id = Convert.ToInt32(user.Id);
            var result = messageManager.TGetMessageWithUserName(user_id);
            TempData["count"] = result.Count();
            return View(result);
        }

		public async Task<IActionResult> AdminSendbox()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var user_id = Convert.ToInt32(user.Id);
			var result = messageManager.TGetSendBoxWithUserName(user_id);
			TempData["SendCount"] = result.Count();
			return View(result);
		}

		public IActionResult AdminMessageDetail(int id)
        {
            var result = messageManager.TGetMessageByMessageId(id);
            return View(result);
        }

        [HttpGet("/Admin/Message/AdminDeleteMessage/{message_id}/")]
        public IActionResult AdminDeleteMessage(int message_id)
        {
            var message=messageManager.TGetByID(message_id);
            messageManager.TDelete(message);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdminSendMessage()
        {
			var user = await _userManager.GetUserAsync(HttpContext.User);
			ViewBag.userMail = user.UserName;
			List<SelectListItem> recieverUsers = (from x in await messageManager.TGetUsersAsync()
												  where x.UserName != user.UserName
												  select new SelectListItem
												  {
													  Text = x.UserName.ToString(),
													  Value = x.Id.ToString()
												  }).ToList();
			//Burası Yukarıde Çektiğimiz Verileri Front-End Tarafına Taşıyoruz.
			ViewBag.RecieverUser = recieverUsers;

			return View();
		}

        [HttpPost]
        public IActionResult AdminSendMessage(Message message)
        {
            using var context = new Context();
            var loggedInUser = _userManager.GetUserId(User);
            int user_id = Convert.ToInt32(loggedInUser);
            message.SenderId = user_id;
            message.MessageStatus = true;
            message.MessageDate = Convert.ToDateTime(DateTime.UtcNow.AddHours(3));
            messageManager.TInsert(message);
            return RedirectToAction("AdminSendbox");
        }


        [HttpGet("/Admin/Message/AdminReplyMessage/{sender_id}/")]
        public async Task<IActionResult> AdminReplyMessage(int sender_id)
        {
            // sender_id'ye göre göndereni al
            var senderUser = await messageManager.TGetUserId(sender_id);
            TempData["ReceiverID"] =senderUser.Id;
            if (senderUser != null)
            {


                ViewBag.senderName = senderUser.UserName;

                var user = await _userManager.GetUserAsync(HttpContext.User);
                ViewBag.UserMail = user.UserName;

                return View();
            }
            else
            {
                // Belirli bir kullanıcı bulunamadığında ne yapılacağına dair bir işlem yapılabilir.
                // Örneğin, bir hata mesajı görüntülenebilir veya başka bir işlem gerçekleştirilebilir.
                return RedirectToAction("HataSayfasi"); // Hata sayfasına yönlendirme örneği
            }
        }

        [HttpPost]
        public IActionResult AdminReplyMessage(Message message)
        {
            using var context = new Context();
            var loggedInUser = _userManager.GetUserId(User);
            int user_id = Convert.ToInt32(loggedInUser);
            message.SenderId = user_id;
            message.ReceiverId = (int)TempData["ReceiverID"];
            message.MessageStatus = true;
            message.MessageDate = Convert.ToDateTime(DateTime.UtcNow.AddHours(3));
            messageManager.TInsert(message);
            return RedirectToAction("AdminInbox");
        }


    }
}
