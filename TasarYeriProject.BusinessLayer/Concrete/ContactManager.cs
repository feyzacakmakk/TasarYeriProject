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
	public class ContactManager : IContactService
	{
		private readonly IContactDal _contactDal;

		public ContactManager(IContactDal contactDal)
		{
			_contactDal = contactDal;
		}

		public void TDelete(Contact t)
		{
			_contactDal.Delete(t);
		}

		public Contact TGetByID(int id)
		{
			return _contactDal.GetByID(id);
		}

		public List<Contact> TGetList()
		{
			return _contactDal.GetList();
		}

        public List<Contact> TGetListByFilter(Expression<Func<Contact, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void TInsert(Contact t)
		{
			_contactDal.Insert(t);
		}

		public void TUpdate(Contact t)
		{
			_contactDal.Update(t);
		}
	}
}
