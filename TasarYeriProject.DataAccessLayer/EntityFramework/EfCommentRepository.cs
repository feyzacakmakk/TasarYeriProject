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
    public class EfCommentRepository : GenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetCommentListWithUserName()
        {
            using var context = new Context();
            var result = context.Comments
                .Include(x => x.AppUser)
                .Include(y => y.Product)
                .OrderByDescending(z => z.CommentDate)
                .ToList();
            return result;
        }

        public List<Comment> GetCommentsForUserProducts(int user_id)
        {
            DateTime date = DateTime.UtcNow.AddHours(3);
            DateTime TodaysDate = DateTime.Today;
            using var context = new Context();
            var userProducts = context.Products.Where(p => p.AppUserId == user_id).Select(p => p.ProductId).ToList();
            var comments = context.Comments
                .Include(x => x.AppUser)
                .Include(y => y.Product)
                .Where(c => userProducts.Contains((int)c.ProductId))
                .OrderByDescending(c => c.CommentDate)
                .Take(3).ToList();
            return comments;
        }
      
        public List<Comment> GetCommentsWithUserName(int product_id)
        {
            using var context=new Context();
            var result=context.Comments.Where(y => y.ProductId == product_id).Include(x=>x.AppUser).ToList();
            return result;
        }
    }
}
