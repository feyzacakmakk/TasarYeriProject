using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.BusinessLayer.ValidationRules.ProductValidationRules;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.DtoLayer.Dtos.ProductDtos;
using TasarYeriProject.EntityLayer.Concrete;
using X.PagedList;

namespace TasarYeriProject.PresantationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ProductController : Controller
    {
        ProductManager productManager=new ProductManager(new EfProductRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        private readonly UserManager<AppUser> _userManager;

        public ProductController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            var value = productManager.TGetListProductByIdWithProductOwner().ToPagedList(page, 4);
            return View(value);
        }

        [HttpGet("/Admin/Product/ProductList/{user_id}/")]
        public IActionResult ProductList(int user_id,int page = 1)
        {
            var value = productManager.TGetListProductByUserId(user_id).ToPagedList(page, 4);
            return View(value);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            List<SelectListItem> categoryValues = (from x in categoryManager.TGetList()

                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();

            ViewBag.categoryValues = categoryValues;
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDtos addProductDtos)
        {
            var loggedInUser = _userManager.GetUserId(User);
            int user_id = Convert.ToInt32(loggedInUser);
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
                var extension = Path.GetExtension(addProductDtos.Image2.FileName);
                var imageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/productphotos/", imageName);
                var stream = new FileStream(location, FileMode.Create);
                addProductDtos.Image2.CopyTo(stream);
                product.ImageUrl2 = "/productphotos/" + imageName;
            }
            product.AppUserId = user_id;
            product.ProductName = addProductDtos.ProductName;
            product.ProductDescription = addProductDtos.ProductDescription;
            product.Price = addProductDtos.Price;
            product.StockQuantity = addProductDtos.StockQuantity;
            product.ProductStatus = true;
            product.CategoryId = addProductDtos.CategoryId;
            product.AddedDate = Convert.ToDateTime(DateTime.UtcNow.AddHours(3));

            productManager.TInsert(product);

            return RedirectToAction("Index");
        }


        [HttpGet("/Admin/Product/ProductDetail/{product_id}/")]
        public IActionResult ProductDetail(int product_id)
        {
            var value = productManager.TGetProductByIdWithProductOwner(product_id);
            return View(value);
        }


        [HttpGet("/Admin/Product/DeleteProduct/{product_id}/")]
        public IActionResult DeleteProduct(int product_id)
        {
            //üründen önce eğer varsa ürüne ait yorumları bulup onları siliyor
            productManager.TDeleteProductWithComments(product_id);
            var result = productManager.TGetProductById(product_id);
            productManager.TDelete(result);
            return RedirectToAction("Index");
        }


        [HttpGet("/Admin/Product/EditProduct/{product_id}/")]
        public IActionResult EditProduct(int product_id)
        {
            // productId kullanarak mevcut ürünü alın
            var product = productManager.TGetProductById(product_id);

            // Kategori listesini hazırlayın ve ViewBag veya ViewData ile görünüme iletin
            List<SelectListItem> categoryValues = (from x in categoryManager.TGetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();

            ViewBag.categoryValues = categoryValues;

            // Mevcut ürün bilgileri ile düzenleme formunu doldurun
            var editProductDtos = new ProductEditDtos
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                // Diğer ürün özelliklerini burada doldurun
            };

            return View(editProductDtos);
        }


        [HttpPost]
        public IActionResult EditProduct(ProductEditDtos editProductDtos)
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

            return RedirectToAction("ProductDetail", "Product", new { product_id = existingProduct.ProductId });


        }

        [HttpGet("/Admin/Product/GetProductPassive/{product_id}")]
        public IActionResult GetProductPassive(int product_id)
        {
            var product = productManager.TGetProductById(product_id);
            if (product.ProductStatus ==true)
            {
                product.ProductStatus = false;
                productManager.TUpdate(product);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("/Admin/Product/GetProductActive/{product_id}")]
        public IActionResult GetProductActive(int product_id)
        {
            var product = productManager.TGetProductById(product_id);
            if (product.ProductStatus == false)
            {
                product.ProductStatus = true;
                productManager.TUpdate(product);
            }
            return RedirectToAction("Index");
        }

    }
}
