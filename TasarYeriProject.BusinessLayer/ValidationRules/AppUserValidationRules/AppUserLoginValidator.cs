using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.DtoLayer.Dtos.AppUserDtos;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.ValidationRules.AppUserValidationRules
{
    public class AppUserLoginValidator:AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş geçilemez.")
                                 .Matches(@"@").WithMessage("Lütfen geçerli bir email adresi giriniz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir email adresi giriniz.");
        }
    }
}
