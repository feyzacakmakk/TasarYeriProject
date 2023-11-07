using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using TasarYeriProject.DtoLayer.Dtos.AppUserDtos;
using TasarYeriProject.EntityLayer.Concrete;

namespace TasarYeriProject.PresantationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if(ModelState.IsValid)
            {
                Random random = new Random();
                int confirmcode = random.Next(100000, 1000000);
                AppUser appUser = new AppUser()
                {
                    UserName = appUserRegisterDto.Username,
                    Name = appUserRegisterDto.Name,
                    Surname = appUserRegisterDto.Surname,
                    Email = appUserRegisterDto.Email,
                    Address = "Adres bilginizi giriniz",
                    CityName="Şehir bilginizi girin.",
                    AboutText="Hakkınızda yazısı",
                    ImageUrl= "/photos/user.png",
                    UserStatus = true,
                ConfirmCode =confirmcode
                };
                var result=await _userManager.CreateAsync(appUser,appUserRegisterDto.Password);
                if(result.Succeeded)
                {
                    //var member = _roleManager.Roles.FirstOrDefault(x => x.Id == 2).ToString();
                    await _userManager.AddToRoleAsync(appUser,"Üye");
                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom=new MailboxAddress("Tasaryeri.com","feyzacakmakk1@gmail.com");
                    //gönderen kişinin kim olduğu ve mailinin ne olduğu
                    MailboxAddress mailboxAddressTo=new MailboxAddress("user",appUser.Email);
                    //gönderdiğimiz mail kime gidecek
                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz: " + confirmcode;
                    mimeMessage.Body=bodyBuilder.ToMessageBody();
                    
                    mimeMessage.Subject = "Tasaryeri.com Onay Kodu";

                    SmtpClient client = new SmtpClient(); //mail transfer protokol nesne örneği alıyoruz
                    client.Connect("smtp.gmail.com", 587, false); //bağlantı kuruyoruz, 587 türkiye port numarası
                    client.Authenticate("feyzacakmakk1@gmail.com", "yhtzvmsjgxgbvszg");
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                    //controllerdan view a veri taşıma yöntemi
                    TempData["Mail"] = appUserRegisterDto.Email;

                    return RedirectToAction("Index","ConfirmMail");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult SellerRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SellerRegister(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int confirmcode = random.Next(100000, 1000000);
                AppUser appUser = new AppUser()
                {
                    UserName = appUserRegisterDto.Username,
                    Name = appUserRegisterDto.Name,
                    Surname = appUserRegisterDto.Surname,
                    Email = appUserRegisterDto.Email,
                    Address = "Adres bilginizi giriniz",
                    CityName = "Şehir bilginizi girin.",
                    AboutText = "Hakkınızda yazısı",
                    ImageUrl = "/photos/user.png",
                    UserStatus=true,
                    ConfirmCode = confirmcode
                };
                var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);
                if (result.Succeeded)
                {
                    //var member = _roleManager.Roles.FirstOrDefault(x => x.Id == 2).ToString();
                    await _userManager.AddToRoleAsync(appUser, "Satıcı");
                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("Tasaryeri.com", "feyzacakmakk1@gmail.com");
                    //gönderen kişinin kim olduğu ve mailinin ne olduğu
                    MailboxAddress mailboxAddressTo = new MailboxAddress("user", appUser.Email);
                    //gönderdiğimiz mail kime gidecek
                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz: " + confirmcode;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    mimeMessage.Subject = "Tasaryeri.com Onay Kodu";

                    SmtpClient client = new SmtpClient(); //mail transfer protokol nesne örneği alıyoruz
                    client.Connect("smtp.gmail.com", 587, false); //bağlantı kuruyoruz, 587 türkiye port numarası
                    client.Authenticate("feyzacakmakk1@gmail.com", "yhtzvmsjgxgbvszg");
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                    //controllerdan view a veri taşıma yöntemi
                    TempData["Mail"] = appUserRegisterDto.Email;

                    return RedirectToAction("Index", "ConfirmMail");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
    }
}
