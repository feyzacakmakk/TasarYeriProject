using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.DtoLayer.Dtos.ProductDtos;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.BusinessLayer.ValidationRules.ProductValidationRules
{
    public class ProductValidator : AbstractValidator<AddProductDtos>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün adı boş geçilemez.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün fiyatı boş geçilemez.");
            RuleFor(x => x.StockQuantity).NotEmpty().WithMessage("Ürün stok adedi boş geçilemez.");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Ürün görseli adı boş geçilemez.");
			RuleFor(x => x.CategoryId)
		   .NotEmpty().WithMessage("Ürün kategorisi boş geçilemez.")
		   .GreaterThan(0).WithMessage("Lütfen bir kategori seçin.");
			RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün açıklaması adı boş geçilemez.");


        }
    }
}
