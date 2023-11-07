using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TasarYeriProject.BusinessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.Concrete;
using TasarYeriProject.DataAccessLayer.EntityFramework;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.ViewComponents.SellerDashboard
{
    public class ValuesTablesViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ProductManager _productManager = new ProductManager(new EfProductRepository());
        public ValuesTablesViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            DateTime TodaysDate = DateTime.Today;
            DateTime FifteenDaysAgo = DateTime.Today.AddDays(-15);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var user_id = Convert.ToInt32(user.Id);
            var userProducts = _productManager.TGetProductsWithProductOwner(user_id);
            var value =_userManager.Users.Count();
            //x=>x.AddedDate== TodaysDate users tablosuna eklenme tarihi ekleyeceğim
            ViewBag.Value = value;
            var totalProductCount=userProducts.Count();
            var productsAddedInLastFifteenDays = userProducts.Count(x=>x.AddedDate>= FifteenDaysAgo);
            int percentage = (int)((double)productsAddedInLastFifteenDays / totalProductCount * 100);

            ViewBag.Percentage = percentage;
            ViewBag.ProductsCount = productsAddedInLastFifteenDays;
            var productSum= userProducts.Sum(x => x.Price);
            ViewBag.ProductSum = productSum;
            return View();
        }

        //son bir ay
        //DateTime birAyOnce = DateTime.Today.AddMonths(-1); 
        //k => k.KayitTarihi >= birAyOnce


        //son 15 gün
        //DateTime onBesGunOnce = DateTime.Today.AddDays(-15);
        //k => k.KayitTarihi >= onBesGunOnce

    }
}
