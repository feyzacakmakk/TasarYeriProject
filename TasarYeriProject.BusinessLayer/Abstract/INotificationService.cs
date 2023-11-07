using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.Abstract
{
    public interface INotificationService:IGenericService<Notification>
    {
        public List<Notification> TGetNotificationsByUserId(int user_id);
    }
}
