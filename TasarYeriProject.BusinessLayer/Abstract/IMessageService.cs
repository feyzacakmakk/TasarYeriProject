using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.Abstract
{
    public interface IMessageService:IGenericService<Message>
    {
        public List<Message> TGetInboxListByUser(int user_id);

        public List<Message> TGetMessageWithUserName(int user_id);

        public Message TGetMessageByMessageId(int id);
        public void TSendMessageByUserName(int user_id);

        public Task<List<AppUser>> TGetUsersAsync();

        public Task<AppUser> TGetUserId(int id);
        public List<Message> TGetSendBoxWithUserName(int user_id);

	}
}
