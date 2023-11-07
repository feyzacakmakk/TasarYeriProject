using FluentValidation.Results;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.BusinessLayer.ValidationRules.ProductValidationRules;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.DtoLayer.Dtos.AppUserDtos;
using TasarYeriProject.EntityLayer.Concrete;
using X.PagedList;
using System.Web;
using TasarYeriProject.DtoLayer.Dtos.ProductDtos;
using System.Xml.Linq;

namespace TasarYeriProject.PresantationLayer.Controllers
{
    public class ProductController : Controller
	{
		ProductManager productManager = new ProductManager(new EfProductRepository());
		CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());

        private readonly UserManager<AppUser> _userManager;

        public ProductController( UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(int page=1)
		{
            var values = productManager.TGetListByFilter(x=>x.ProductStatus==true).ToPagedList(page, 6);
            return View(values);

        }

		[HttpGet("/Product/ProductDetail/{product_id}/")]
		public async Task<IActionResult> ProductDetail(int product_id)
		{
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ViewBag.UserFullName = $"{user.Name} {user.Surname}";
                ViewBag.UserId = user.Id;
            }
            //ViewBag.product_id = product_id;
            var values=productManager.TGetProductByIdWithProductOwner(product_id);
            if (values != null)
            {
                var comment = values.Comments?.Count() ?? 0;
                ViewBag.Comments = comment;
            }
            else
            {
                // values null ise, burada nasıl ele alınacağını belirleyebilirsiniz.
                // Örneğin, ViewBag.Comments'e varsayılan bir değer atayabilirsiniz.
                ViewBag.Comments = 0;
            }
            
            return View(values);
		}

		[HttpGet("/Product/ProductByCategoryId/{category_id}/")]
		public IActionResult ProductByCategoryId(int category_id)
		{
			var values = productManager.TGetProductsByCategoryId(category_id);
			return View(values);
		}


		public IActionResult Favorites()
		{
			var values = productManager.TGetList();
			return View(values);
		}







		public IActionResult SellerDashboardProductList(int page = 1)
        {
            var loggedInUser = _userManager.GetUserId(User);
            int user_id = Convert.ToInt32(loggedInUser);
			var value = productManager.TGetProductsWithProductOwner(user_id).ToPagedList(page, 8);
			return View(value);
        }

        [HttpGet("/Product/SellerDashboardAddProduct/")]
        public IActionResult SellerDashboardAddProduct()
        {
            //List<SelectListItem> categoryValues = (from x in categoryManager.TGetList()

            //                                       select new SelectListItem
            //                                       {
            //                                           Text = x.CategoryName,
            //                                           Value = x.CategoryId.ToString()
            //                                       }).ToList();

			//ViewBag.categoryValues = categoryValues;

			ViewBag.categoryValues = new SelectList(categoryManager.TGetList(), "CategoryId", "CategoryName");

			return View();
        }

		[HttpPost("/Product/SellerDashboardAddProduct/")]
		public IActionResult SellerDashboardAddProduct(AddProductDtos addProductDtos)
		{
			var loggedInUser = _userManager.GetUserId(User);
			int user_id = Convert.ToInt32(loggedInUser);
			ProductValidator productValidator = new ProductValidator();
			ValidationResult result = productValidator.Validate(addProductDtos);
			if (result.IsValid)
			{
				Product product = new Product();
				if (addProductDtos.Image != null)
				{
					var extension = Path.GetExtension(addProductDtos.Image.FileName);
					var imageName = Guid.NewGuid() + extension;
					var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/productphotos/", imageName);
					var stream = new FileStream(location, FileMode.Create);
					addProductDtos.Image.CopyTo(stream);
					product.ImageUrl = "/productphotos/" + imageName;
				}

				if (addProductDtos.Image2 != null)
				{
					var extension = Path.GetExtension(addProductDtos.Image.FileName);
					var imageName2 = Guid.NewGuid() + extension;
					var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/productphotos/", imageName2);
					var stream = new FileStream(location, FileMode.Create);
					addProductDtos.Image2.CopyTo(stream);
					product.ImageUrl2 = "/productphotos/" + imageName2;
				}

				product.AppUserId = user_id;
				product.ProductName = addProductDtos.ProductName;
				product.ProductDescription = addProductDtos.ProductDescription;
				product.Price = addProductDtos.Price;
				product.StockQuantity = addProductDtos.StockQuantity;
				product.ProductStatus = true;
				product.CategoryId = addProductDtos.CategoryId;
				product.AddedDate = Convert.ToDateTime(DateTime.UtcNow.AddHours(3));

				// Ürünü veritabanına ekleyin
				productManager.TInsert(product);

				return RedirectToAction("SellerDashboardProductList");
			}

			return View(addProductDtos);
		}




		[HttpGet("/Product/SellerDashboardDeleteProduct/{product_id}/")]
        public IActionResult SellerDashboardDeleteProduct(int product_id)
        {
            //üründen önce eğer varsa ürüne ait yorumları bulup onları siliyor
            productManager.TDeleteProductWithComments(product_id);
            var result = productManager.TGetProductById(product_id);
            productManager.TDelete(result);
            return RedirectToAction("SellerDashboardProductList");
        }


        [HttpGet("/Product/SellerDashboardProductDetail/{product_id}/")]
        public IActionResult SellerDashboardProductDetail(int product_id)
        {
            TempData["productId"]=product_id;
            var value = productManager.TGetProductByIdWithProductOwner(product_id);
            return View(value);
        }


        [HttpGet("/Product/SellerDashboardEditProduct/{productId}/")]
        public IActionResult SellerDashboardEditProduct(int productId)
        {
            var product = productManager.TGetProductById(productId);

            List<SelectListItem> categoryValues = (from x in categoryManager.TGetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();

            ViewBag.categoryValues = categoryValues;

            var editProductDtos = new ProductEditDtos
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
            };

            return View(editProductDtos);
        }

        [HttpPost]
        public IActionResult SellerDashboardEditProduct(ProductEditDtos editProductDtos)
        {
                var existingProduct = productManager.TGetProductById(editProductDtos.ProductId);

                existingProduct.ProductName = editProductDtos.ProductName;
                existingProduct.ProductDescription = editProductDtos.ProductDescription;
                existingProduct.Price = editProductDtos.Price;
                existingProduct.StockQuantity = editProductDtos.StockQuantity;
                if (editProductDtos.Image != null)
                {
                    var extension = Path.GetExtension(editProductDtos.Image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/productphotos/", imageName);
                    var stream = new FileStream(location, FileMode.Create);
                    editProductDtos.Image.CopyTo(stream);
                    existingProduct.ImageUrl = "/productphotos/" + imageName;
                }
                if (editProductDtos.Image2 != null)
                {
                    var extension = Path.GetExtension(editProductDtos.Image2.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/productphotos/", imageName);
                    var stream = new FileStream(location, FileMode.Create);
                    editProductDtos.Image2.CopyTo(stream);
                    existingProduct.ImageUrl2 = "/productphotos/" + imageName;
                }
                existingProduct.CategoryId = editProductDtos.CategoryId;
                productManager.TUpdate(existingProduct);
                
                return RedirectToAction("SellerDashboardProductList");
            
        }



        [HttpGet("/Product/GetProductPassive/{product_id}/")]
        public IActionResult GetProductPassive(int product_id)
        {
            var product = productManager.TGetProductById(product_id);
            if (product.ProductStatus == true)
            {
                product.ProductStatus = false;
                productManager.TUpdate(product);
            }
            return RedirectToAction("SellerDashboardProductDetail","Product", new {product_id});
        }

        [HttpGet("/Product/GetProductActive/{product_id}/")]
        public IActionResult GetProductActive(int product_id)
        {
            var product = productManager.TGetProductById(product_id);
            if (product.ProductStatus == false)
            {
                product.ProductStatus = true;
                productManager.TUpdate(product);
            }
            return RedirectToAction("SellerDashboardProductDetail", "Product", new { product_id });
        }


    }
}
