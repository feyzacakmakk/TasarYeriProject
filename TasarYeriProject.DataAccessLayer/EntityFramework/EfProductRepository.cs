using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
	public class EfProductRepository : GenericRepository<Product>, IProductDal
	{
		public List<Product> GetProductsWithProductOwner(int user_id)
		{
			using var context=new Context();
            var result = context.Products.Where(x => x.AppUserId == user_id)
                .Include(y => y.Category).ToList();
            return result;
        }

        public List<Product> GetListProductByUserId(int user_id)
        {
            using var context = new Context();
            var result = context.Products.Where(x => x.AppUserId == user_id && x.ProductStatus == true)
                .Include(y=>y.AppUser)
                .Include(z => z.Category).ToList();
            return result;
        }

        public Product GetProductById(int product_id)
		{

			using var context = new Context();
			var v = context.Users.Count();
			var result = context.Set<Product>().Find(product_id);
			return result;

		}

		public List<Product> GetProductsByCategoryId(int category_id)
		{
			using var context = new Context();
			var result = context.Products.Where(p => p.CategoryId == category_id && p.ProductStatus == true).ToList();
			return result;
		}

        public Product GetProductByIdWithProductOwner(int product_id)
        {
            using var context = new Context();
			var result = context.Products.Where(p => p.ProductId == product_id)
                .Include(y=>y.Comments)
                .Include(x => x.AppUser).FirstOrDefault();
            return result;
        }

        public List<Product> GetListProductByIdWithProductOwner()
        {
            using var context = new Context();
            var result = context.Products
                .Include(x => x.AppUser) // AppUser tablosunu dahil ettik
                .Include(x => x.Category) // Category tablosunu dahil etttik
                .OrderByDescending(x => x.AddedDate) //en yakın tarihten listelemeye başlıyor
                .ToList();

            return result;
        }
       

        public List<Product> GetProductsByToday(DateTime today)
        {
            using var context = new Context();
			var result = context.Products.Where(p => p.AddedDate == today && p.ProductStatus == true).ToList();
			return result;
        }

        public List<Product> GetProductsWithProductOwnerToday(int user_id)
        {
            DateTime TodaysDate = DateTime.Today;
            using var context = new Context();
            var result = context.Products.Where(x => x.AppUserId == user_id && x.AddedDate ==TodaysDate && x.ProductStatus == true).Include(y => y.Category).ToList();
            return result;
        }

        public void DeleteProductWithComments(int product_id)
        {
            using var context = new Context();
            var product = context.Products.Include(p => p.Comments).FirstOrDefault(p => p.ProductId == product_id && p.ProductStatus == true);

            if (product != null)
            {
                context.Comments.RemoveRange(product.Comments);
                context.SaveChanges();
            }
        }

      
    }
}
