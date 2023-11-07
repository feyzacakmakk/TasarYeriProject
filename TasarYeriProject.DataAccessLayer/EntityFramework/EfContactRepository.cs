using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.DataAccessLayer.Abstract;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.EntityFramework
{
	public class EfContactRepository:GenericRepository<Contact>,IContactDal
	{
	}
}
