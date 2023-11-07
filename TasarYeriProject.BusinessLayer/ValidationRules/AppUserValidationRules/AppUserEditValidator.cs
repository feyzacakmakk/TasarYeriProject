using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.DtoLayer.Dtos.AppUserDtos;

namespace TasarYeriProject.BusinessLayer.ValidationRules.AppUserValidationRules
{
    public class AppUserEditValidator : AbstractValidator<AppUserEditDto>
    {
        public AppUserEditValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyadı alanı boş geçilemez.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş geçilemez.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrarı alanı boş geçilemez.");
            RuleFor(x => x.ConfirmPassword).Equal(y => y.Password).WithMessage("Parolalarınız eşleşmiyor.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir email adresi giriniz.");
        }
    }
}
