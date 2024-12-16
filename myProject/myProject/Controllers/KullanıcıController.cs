using Microsoft.AspNetCore.Mvc;
using myProject.Models;

namespace myProject.Controllers
{
    public class KullanıcıController : Controller
    {
        private readonly myyDbContext _context;
        public KullanıcıController(myyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KayitOl(string kullaniciAdi, string kullaniciSifre, string eposta)
        {
            var kullanıcı = _context.Kullanıcı.FirstOrDefault(k => k.eposta == eposta);
            if(kullanıcı != null)
            {
                TempData["msj"] = "Bu eposta adresi zaten kullanılıyor.";
                RedirectToAction("KayitOl");
            }
            kullanıcı = _context.Kullanıcı.FirstOrDefault(k=>k.kullaniciAdi == kullaniciAdi);
            if(kullanıcı != null)
            {
                TempData["msj"] = "Bu kullanıcı adı zaten kullanılıyor.";
                RedirectToAction("KayitOl");
            }
            kullanıcı = new Kullanıcı { kullaniciAdi = kullaniciAdi, kullaniciSifre = kullaniciSifre ,eposta = eposta ,rol = "Member"};
            _context.Kullanıcı.Add(kullanıcı);
            
            await _context.SaveChangesAsync();

            return RedirectToAction("GirisYap");
        }
        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult GirisYap(string kullaniciAdi, string sifre)
        {
            var kullanici = _context.Kullanıcı
                .FirstOrDefault(k => k.kullaniciAdi == kullaniciAdi && k.kullaniciSifre == sifre);
            if (kullanici != null) {
                if(kullanici.rol == "Admin")
                {
                    CookieOptions options1 = new CookieOptions();
                    options1.Expires = DateTime.Now.AddMinutes(100);
                    HttpContext.Response.Cookies.Append("KullaniciID", kullanici.kullaniciID.ToString(), options1);
                    return RedirectToAction("AdminPaneli", "BerberSalonu");
                }
                CookieOptions options2 = new CookieOptions();
                options2.Expires = DateTime.Now.AddDays(3);
                HttpContext.Response.Cookies.Append("KullaniciID", kullanici.kullaniciID.ToString(), options2);
                return RedirectToAction("Index", "BerberSalonu");
            }
            TempData["msj"] = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }
    }
}
