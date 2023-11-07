using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.BusinessLayer.Abstract;
using TasarYeriProject.DataAccessLayer.Abstract;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Task<List<AppUser>> TGetUsersAsync()
        {
            return _messageDal.GetUsersAsync();
        }
        //public async Task<List<AppUser>> GetUsersAsync()
        //{
        //    using (var context = new Context())
        //    {
        //        return await context.Users.ToListAsync();
        //    }
        //}

        public Task<AppUser> TGetUserId(int id)
        {
            return _messageDal.GetUserId(id);
        }
        //public async Task<AppUser> GetUserId(int id)
        //{
        //    using (var context = new Context())
        //    {
        //        return await context.Users.FindAsync(id);
        //    }
        //}
        public void TDelete(Message t)
        {
            _messageDal.Delete(t);
        }

        public Message TGetByID(int id)
        {
            return _messageDal.GetByID(id);
        }

        public List<Message> TGetInboxListByUser(int user_id)
        {
            return _messageDal.GetInboxListByUser(user_id);
        }

        public List<Message> TGetList()
        {
            return _messageDal.GetList();
        }

        public List<Message> TGetListByFilter(Expression<Func<Message, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Message TGetMessageByMessageId(int id)
        {
            return _messageDal.GetMessageByMessageId(id);
        }
		public List<Message> TGetMessageWithUserName(int user_id)
        {
            return _messageDal.GetMessageWithUserName(user_id);
        }

        public void TInsert(Message t)
        {
            _messageDal.Insert(t);
        }

        public void TSendMessageByUserName(int user_id)
        {
            _messageDal.SendMessageByUserName(user_id);
        }

        public void TUpdate(Message t)
        {
            _messageDal.Update(t);
        }

		public List<Message> TGetSendBoxWithUserName(int user_id)
		{
            return _messageDal.GetSendBoxWithUserName(user_id);
		}
	}
}
