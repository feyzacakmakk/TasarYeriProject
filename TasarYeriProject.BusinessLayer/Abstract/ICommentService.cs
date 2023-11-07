using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.Abstract
{
    public interface ICommentService:IGenericService<Comment>
    {
        public List<Comment> TGetCommentsWithUserName(int product_id);
        public List<Comment> TGetCommentListWithUserName();

        public List<Comment> TGetCommentsForUserProducts(int user_id);
    }
}
