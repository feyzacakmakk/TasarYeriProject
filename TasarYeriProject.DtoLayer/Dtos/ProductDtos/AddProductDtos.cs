using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasarYeriProject.DtoLayer.Dtos.ProductDtos
{
    public class AddProductDtos
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime AddedDate { get; set; }
        public bool ProductStatus { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Image2 { get; set; }

		[Required(ErrorMessage = "Ürün kategori seçimi zorunludur.")]
		public int CategoryId { get; set; }
    }
}
