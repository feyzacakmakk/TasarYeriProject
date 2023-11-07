using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Abstract;

namespace TasarYeriProject.EntityLayer.Concrete
{
    public class Comment : IEntity
    {
        [Key]
        public int CommentId { get; set; }
        public string? CommentContent { get; set; }
        public DateTime CommentDate { get; set; }
        public bool? CommentStatus { get; set; }

        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


    }
}
