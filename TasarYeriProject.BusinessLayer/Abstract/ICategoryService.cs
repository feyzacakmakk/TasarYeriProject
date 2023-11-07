using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.Abstract
{
    public interface ICategoryService:IGenericService<Category>
    {
        public List<Category> TGetCategories();
	}
}
