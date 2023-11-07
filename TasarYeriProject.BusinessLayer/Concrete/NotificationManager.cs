using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.BusinessLayer.Abstract;
using TasarYeriProject.DataAccessLayer.Abstract;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationDal _notificationDal;

        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public void TDelete(Notification t)
        {
            throw new NotImplementedException();
        }

        public Notification TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        //noti.manager
        public List<Notification> TGetList()
        {
            return _notificationDal.GetList();
        }

        public List<Notification> TGetListByFilter(Expression<Func<Notification, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Notification> TGetNotificationsByUserId(int user_id)
        {
            return _notificationDal.GetNotificationsByUserId(user_id);
        }

        public void TInsert(Notification t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Notification t)
        {
            throw new NotImplementedException();
        }
    }
}
