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
    public class AppUserManager : IAppUserService
    {
        //Dal'daki metotları çağırarak içlerini doldurucaz
        private readonly IAppUserDal _appUserDal;

        //dependency injection
        public AppUserManager(IAppUserDal appUserDal)
        {
            _appUserDal = appUserDal;
        }

        public List<Product> TGetProductsById(int user_id)
        {
            return _appUserDal.GetProductsById(user_id);
        }

        public void TDelete(AppUser t)
        {
            _appUserDal.Delete(t);
        }

        public AppUser TGetByID(int id)
        {
            return _appUserDal.GetByID(id);
        }

        public List<AppUser> TGetList()
        {
            return _appUserDal.GetList();
        }

        public AppUser TGetUserById(int user_id)
        {
            return _appUserDal.GetUserById(user_id);
        }

        public void TInsert(AppUser t)
        {
            _appUserDal.Insert(t);
        }

        public void TUpdate(AppUser t)
        {
            _appUserDal.Update(t);
        }

        public Product TGetProductById(int product_id)
        {
            return _appUserDal.GetProductById(product_id);
        }

        public List<AppUser> TGetListByFilter(Expression<Func<AppUser, bool>> filter)
        {
            return _appUserDal.GetListByFilter(filter);
        }

        public void TDeleteUserWithMessage(int user_id)
        {
            _appUserDal.DeleteUserWithMessage(user_id);
        }
    }
}
