using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.DataAccessLayer.Abstract;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.EntityFramework
{
    public class EfMessageRepository : GenericRepository<Message>, IMessageDal
    {
        public List<Message> GetInboxListByUser(int user_id)
        {
            using var context = new Context();
            var result = context.Messages.Where(x=> x.ReceiverId == user_id).ToList();
            return result;
        }

        public Message GetMessageByMessageId(int id)
        {
            using var context = new Context();
            var result = context.Messages.Include(x => x.SenderUser)
                .Include(y=>y.ReceiverUser)
                .Where(x => x.MessageId == id).FirstOrDefault();
            return result;
        }

        public List<Message> GetMessageWithUserName(int user_id)
        {
            //gelen mesajları listelemek istediğim için senderuser u ekliyorum
            using var context = new Context();
            var result = context.Messages.Include(x=>x.SenderUser).Where(x => x.ReceiverId == user_id)
                .OrderByDescending(y=>y.MessageDate)
                .ToList();
            return result;
        }

		public List<Message> GetSendBoxWithUserName(int user_id)
		{
			using var context = new Context();
			var result = context.Messages.Include(x => x.ReceiverUser).Where(x => x.SenderId == user_id)
				.OrderByDescending(y => y.MessageDate)
				.ToList();
			return result;
		}

		public async Task<AppUser> GetUserId(int id)
        {
            using (var context = new Context())
            {
                return await context.Users.FindAsync(id);
            }
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            using (var context = new Context())
            {
                return await context.Users.ToListAsync();
            }
        }

        public void SendMessageByUserName(int user_id)
        {
            using var context = new Context();
            var userMail = context.Users.Where(x => x.Id == user_id).Select(y => y.Email).FirstOrDefault();
            var user=context.Users.Where(x=>x.Email == userMail).Select(y=>y.Id).FirstOrDefault();
        }


        //public Message SendMessageByUserName(int user_id)
        //{
        //    //using var context=new Context();
        //    //var userMail = context.Users.Where(x => x.Id == user_id).Select(y=>y.Email).FirstOrDefault();
        //    ////var user=context.Users.Where(x=>x.Email == userMail).Select(y=>y.Id).FirstOrDefault();
        //    //return userMail;
        //    return re
        //}
    }
}
