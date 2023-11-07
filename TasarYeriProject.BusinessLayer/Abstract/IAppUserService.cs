using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.Abstract
{
    public interface IAppUserService:IGenericService<AppUser>
    {
        public AppUser TGetUserById(int user_id);

        public List<Product> TGetProductsById(int user_id);

        public Product TGetProductById(int product_id);

        public void TDeleteUserWithMessage(int user_id);
    }
}
