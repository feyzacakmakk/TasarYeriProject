using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.DataAccessLayer.Abstract;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.EntityFramework
{
    public class EfAppUserRespository : GenericRepository<AppUser>, IAppUserDal
    {
        //public void DeleteUserWithMessage(int user_id)
        //{
        //    using var context = new Context();
        //    var user = context.Users.Include(p => p.MessageReceiver)
        //        .Include(x=>x.MessageSender)
        //        .FirstOrDefault(p => p.Id == user_id);

        //    if (user != null)
        //    {
        //        context.Users.RemoveRange((AppUser)user.MessageSender);
        //        context.Users.RemoveRange((AppUser)user.MessageReceiver);
        //        context.SaveChanges();
        //    }
        //}

        public void DeleteUserWithMessage(int user_id)
        {
            using var context = new Context();

            // Kullanıcının gönderdiği mesajları sil
            var sentMessages = context.Messages.Where(m => m.SenderUser.Id == user_id);
            context.Messages.RemoveRange(sentMessages);

            // Kullanıcının aldığı mesajları sil
            var receivedMessages = context.Messages.Where(m => m.ReceiverUser.Id == user_id);
            context.Messages.RemoveRange(receivedMessages);
        }


        public Product GetProductById(int product_id)
        {
            using var context = new Context();
            var result = context.Products.Where(p => p.ProductId == product_id).Include(x => x.AppUser).Include(x => x.Category).FirstOrDefault();
            return result;
        }

        public List<Product> GetProductsById(int user_id)
        {
            using var context = new Context();
            var result = context.Products.Where(x=>x.AppUserId == user_id).Include(y => y.Category).ToList();
            return result;
        }
        public AppUser GetUserById(int user_id)
        {
            using var context = new Context();
            var result = context.Users.Find(user_id);
            //User=AppUser, sisteme giriş yapan kullanıcının adını getirir.
            //context.Users.FirstOrDefault(x=>x.Id == user_id)
            return result;

        }

    }
}
