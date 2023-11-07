using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Abstract;

namespace TasarYeriProject.EntityLayer.Concrete
{
    public class Message : IEntity
    {
        [Key]
        public int MessageId { get; set; }
        public int? SenderId { get; set; } //gönderen
        public int? ReceiverId { get; set; } //alıcı
        public string? Subject { get; set; }
        public string? MessageDetails { get; set; }
        public DateTime MessageDate { get; set; }
        public bool MessageStatus { get; set; }

        public AppUser? SenderUser { get; set; }
        public AppUser? ReceiverUser { get; set; }
    }
}
