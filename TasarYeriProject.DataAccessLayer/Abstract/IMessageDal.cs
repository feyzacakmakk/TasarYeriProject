using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.Abstract
{
    public interface IMessageDal:IGenericDal<Message>
    {
        public List<Message> GetInboxListByUser(int user_id);

        public List<Message> GetMessageWithUserName(int user_id);

        public List<Message> GetSendBoxWithUserName(int user_id);


		public Message GetMessageByMessageId(int id);
        public void SendMessageByUserName(int user_id);

        public Task<List<AppUser>> GetUsersAsync();
        public Task<AppUser> GetUserId(int id);

    }
}
