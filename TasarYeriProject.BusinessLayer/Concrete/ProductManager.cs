using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.BusinessLayer.Abstract;
using TasarYeriProject.DataAccessLayer.Abstract;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.Concrete
{
	public class ProductManager : IProductService
	{
		//Dal'daki metotları çağırarak içlerini doldurucaz
		private readonly IProductDal _productDal;

		//dependency injection
		public ProductManager(IProductDal productDal)
		{
			_productDal = productDal;
		}

		public Product TGetProductById(int product_id)
		{
			var result=_productDal.GetByID(product_id);
			return result;
		}

        public void TDelete(Product t)
		{
			_productDal.Delete(t);
		}

		public Product TGetByID(int id)
		{
			return _productDal.GetByID(id);
		}

		public List<Product> TGetList()
		{
			return _productDal.GetList();
		}

		public void TInsert(Product t)
		{
			_productDal.Insert(t);
		}

		public void TUpdate(Product t)
		{
			_productDal.Update(t);
		}

		public List<Product> TGetProductsWithProductOwner(int user_id)
		{
			return _productDal.GetProductsWithProductOwner(user_id);
		}

		public List<Product> TGetProductsByCategoryId(int category_id)
		{
			return _productDal.GetProductsByCategoryId(category_id);
		}

        public Product TGetProductByIdWithProductOwner(int product_id)
        {
            return _productDal.GetProductByIdWithProductOwner(product_id);
        }

        public List<Product> TGetProductsByToday(DateTime today)
        {
			return _productDal.GetProductsByToday(today);
        }

        public List<Product> TGetProductsWithProductOwnerToday(int user_id)
        {
            return _productDal.GetProductsWithProductOwnerToday(user_id);
        }

        public void TDeleteProductWithComments(int product_id)
        {
            _productDal.DeleteProductWithComments(product_id);
        }

        public List<Product> TGetListProductByIdWithProductOwner()
        {
            return _productDal.GetListProductByIdWithProductOwner();
        }

        public List<Product> TGetListProductByUserId(int user_id)
        {
			return _productDal.GetListProductByUserId(user_id);
        }

        public List<Product> TGetListByFilter(Expression<Func<Product, bool>> filter)
        {
            return _productDal.GetListByFilter(filter);
        }
    }
}
