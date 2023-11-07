using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasarYeriProject.DtoLayer.Dtos.AppUserDtos
{
    public class AppUserEditDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string AboutText { get; set; }
        public IFormFile Image { get; set; }
    }
}
