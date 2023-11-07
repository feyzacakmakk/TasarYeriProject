using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasarYeriProject.EntityLayer.Abstract;

namespace TasarYeriProject.EntityLayer.Concrete
{
	public class Product:IEntity
	{
		[Key]
		public int ProductId { get; set; }
		public string? ProductName { get; set; }
		public string? ProductDescription { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime AddedDate { get; set; }
		public string? ImageUrl { get; set; }
		public string? ImageUrl2 { get; set; }
        public bool ProductStatus { get; set; }

        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Comment>? Comments { get; set; }



    }
}
