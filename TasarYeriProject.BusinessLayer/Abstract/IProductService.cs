using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.Abstract
{
	public interface IProductService:IGenericService<Product>
	{
		public List<Product> TGetProductsWithProductOwner(int user_id);
		public Product TGetProductById(int product_id);

		public List<Product> TGetProductsByCategoryId(int category_id);

        public Product TGetProductByIdWithProductOwner(int product_id);
		public List<Product> TGetProductsByToday(DateTime today);

		public List<Product> TGetProductsWithProductOwnerToday(int user_id);

        public void TDeleteProductWithComments(int product_id);

		public List<Product> TGetListProductByUserId(int user_id);
        public List<Product> TGetListProductByIdWithProductOwner();


    }
}
