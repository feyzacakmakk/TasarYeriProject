using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasarYeriProject.EntityLayer.Concrete
{
	public class AppUser :IdentityUser<int>
	{
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string ImageUrl { get; set; }
        public string AboutText { get; set; }
        public int ConfirmCode{ get; set; }
        public bool UserStatus { get; set; }

        public List<Product>? Products { get; set; }
        public List<Comment>? Comments { get; set; }


        public List<Notification>? Notifications { get; set; }

        
        [InverseProperty("SenderUser")]
        public virtual ICollection<Message>? MessageSender { get; set; }

        [InverseProperty("ReceiverUser")]
        public virtual ICollection<Message>? MessageReceiver { get; set; }

    }
}
