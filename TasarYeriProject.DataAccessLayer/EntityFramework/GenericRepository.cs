using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.DataAccessLayer.Abstract;
using TasarYeriProject.DataAccessLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.EntityFramework
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            using var context = new Context();
            //set ile yukarıdan gelen T değerine göre ayarla ve t ne ise onu sil dedik
            context.Set<T>().Remove(t);
            context.SaveChanges();
        }

        public T GetByID(int id)
        {
            using var context = new Context();
            return context.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            using var context = new Context();
            return context.Set<T>().ToList();
        }

        public List<T> GetListByFilter(Expression<Func<T, bool>> filter)
        {
            using var context = new Context();
            return context.Set<T>().Where(filter).ToList();
        }

        public void Insert(T t)
        {
            using var context = new Context();
            context.Set<T>().Add(t);
            context.SaveChanges();
        }

        public void Update(T t)
        {
            using var context = new Context();
            context.Set<T>().Update(t);
            context.SaveChanges();
        }
    }
}
