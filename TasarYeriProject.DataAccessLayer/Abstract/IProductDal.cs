using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.Abstract
{
	public interface IProductDal:IGenericDal<Product>
	{
        public Product GetProductById(int product_id);
        public List<Product> GetProductsWithProductOwner(int user_id);
        public List<Product> GetProductsWithProductOwnerToday(int user_id);
        public Product GetProductByIdWithProductOwner(int product_id);
        public void DeleteProductWithComments(int product_id);

		public List<Product> GetProductsByCategoryId(int category_id);
		public List<Product> GetProductsByToday(DateTime date);

        public List<Product> GetListProductByIdWithProductOwner();
        public List<Product> GetListProductByUserId(int user_id);

    }
}
