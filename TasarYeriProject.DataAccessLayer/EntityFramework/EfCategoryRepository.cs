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
    public class EfCategoryRepository : GenericRepository<Category>, ICategoryDal
    {
        public List<Category> GetCategories()
        {
            using var context=new Context();
            var result=context.Categories.ToList();
            return result;
        }

		
	}
}
