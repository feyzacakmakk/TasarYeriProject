using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.Abstract
{
    public interface INotificationDal:IGenericDal<Notification>
    {
        public List<Notification> GetNotificationsByUserId(int user_id);
    }
}
