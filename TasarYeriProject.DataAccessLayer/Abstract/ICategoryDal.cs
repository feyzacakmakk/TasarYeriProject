using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.Abstract
{
    public interface ICategoryDal:IGenericDal<Category>
    {
        public List<Category> GetCategories();

    }
}
