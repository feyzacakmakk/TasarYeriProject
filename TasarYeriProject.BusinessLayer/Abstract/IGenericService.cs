using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TasarYeriProject.BusinessLayer.Abstract
{
	public interface IGenericService<T> where T : class
	{
		//IGenericDal'daki metotların aynısını yazıyoruz ama ondan ayırmak için başına T ekliyoruz
		void TInsert(T t);
		void TDelete(T t);
		void TUpdate(T t);
		T TGetByID(int id); //id'ye göre getir
		List<T> TGetList(); // listeyi getir

        List<T> TGetListByFilter(Expression<Func<T, bool>> filter);
    }
}
