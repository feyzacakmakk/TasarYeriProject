using System.ComponentModel.DataAnnotations;

namespace TasarYeriProject.PresantationLayer.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Lütfen rol adı giriniz")]
        public string Name { get; set; }
    }
}
