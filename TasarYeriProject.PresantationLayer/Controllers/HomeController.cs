using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.PresantationLayer.Models;

namespace TasarYeriProject.PresantationLayer.Controllers
{

	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			using var context=new Context();
			var allProducts = context.Products.Where(x=>x.ProductStatus==true).ToList();
			var random=new Random();
			var result = allProducts.OrderBy(x => random.Next()).Take(8).ToList();
			//ürünleri random şekilde alıp 8 tanesini listeye çeviriyor
			return View(result);
		}


	}
}