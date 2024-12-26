using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using myProject.Models;

namespace myProject.Controllers
{
    public class BerberSalonuController : Controller
    {
        private readonly myyDbContext _context;
        public BerberSalonuController(myyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.IsLoggedIn = false; 
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if(kullaniciIDCookie != null)
            {
                
                int kullaniciID = int.Parse(kullaniciIDCookie);
                var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
                if(kullanici != null){
                    ViewBag.KullaniciAdi = kullanici.kullaniciAdi;
                    ViewBag.IsLoggedIn = true;
                    if(kullanici.rol == "Admin")
                    {
                        ViewBag.Role = "Admin";
                    }
                }

            }
            return View();
        }
        [HttpGet]
        public IActionResult AdminPaneli()
        {
            ViewBag.IsLoggedIn = false;
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie != null)
            {
                int kullaniciID = int.Parse(kullaniciIDCookie);
                var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
                if (kullanici != null)
                {
                    ViewBag.IsLoggedIn = true;
                    if (kullanici.rol == "Admin")
                    {
                        ViewBag.Role = "Admin";
                        var berberSalonlari = _context.BerberSalonları.ToList(); // Veritabanından salonları çekiyoruz
                        if (berberSalonlari.Any())
                        {
                            return View(berberSalonlari);
                        }
                        TempData["msj"] = "Lütfen salon ekleyerek başlayınız.";
                        return RedirectToAction("BerberSalonuEkle");
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult BerberSalonuEkle()
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index");
            if (kullanici.rol == "Member") return RedirectToAction("Index");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BerberSalonuEkle(string salonAd,string salonAdres,string salonNumara,string salonEposta,TimeOnly baslangicSaati,TimeOnly bitisSaati)
        {
            var Berbersalonu = new Berbersalonu { salonAd = salonAd, salonAdres = salonAdres, 
                salonNumara = salonNumara, salonEposta = salonEposta, 
                baslangicSaati = baslangicSaati, bitisSaati = bitisSaati };
            _context.BerberSalonları.Add(Berbersalonu);
            await _context.SaveChangesAsync();
            TempData["msj"] = "Yeni bir berber salonu başarıyla oluşturulmuştur.";
            return RedirectToAction("AdminPaneli");
        }
    }
}
