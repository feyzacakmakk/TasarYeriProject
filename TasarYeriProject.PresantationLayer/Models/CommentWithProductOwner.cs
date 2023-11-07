using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.Models
{
    public class CommentWithProductOwner
    {
        public Comment Comment { get; set; } // Yorum bilgisi
        public AppUser ProductOwner { get; set; } // Ürünün sahibi (kullanıcı) bilgisi
    }
}
