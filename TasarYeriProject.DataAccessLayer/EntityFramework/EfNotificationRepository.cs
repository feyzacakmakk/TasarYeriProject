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
    public class EfNotificationRepository : GenericRepository<Notification>,INotificationDal
    {

        //notificationrepository
        public List<Notification> GetList()
        {
            using var context = new Context();
            var result = context.Notifications.ToList();
            return result;

        }

        public List<Notification> GetNotificationsByUserId(int user_id)
        {
            using var context = new Context();
            var result = context.Notifications.Where(x => x.AppUserId == user_id)
                .Include(y=>y.AppUser)
                .ToList();
            return result;
        }
    }
}
