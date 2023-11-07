using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.Abstract
{
    public interface ICommentDal:IGenericDal<Comment>
    {
        public List<Comment> GetCommentsWithUserName(int product_id);
        public List<Comment> GetCommentListWithUserName();

        public List<Comment> GetCommentsForUserProducts(int user_id);
    }
}
