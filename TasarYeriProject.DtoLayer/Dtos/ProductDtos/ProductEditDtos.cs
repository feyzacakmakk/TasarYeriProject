using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasarYeriProject.DtoLayer.Dtos.ProductDtos
{
    public class ProductEditDtos
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime AddedDate { get; set; }
        public bool ProductStatus { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Image2 { get; set; }

        public int CategoryId { get; set; }
    }
}
