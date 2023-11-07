using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.DataAccessLayer.Abstract
{
    public interface IAppUserDal:IGenericDal<AppUser>
    {
        public AppUser GetUserById(int user_id);

        public List<Product> GetProductsById(int user_id);

        public Product GetProductById(int product_id);

        public void DeleteUserWithMessage(int product_id);
    }
}
