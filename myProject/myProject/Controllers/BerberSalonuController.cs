using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (kullaniciIDCookie != null)
            {

                int kullaniciID = int.Parse(kullaniciIDCookie);
                var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
                if (kullanici != null)
                {
                    ViewBag.KullaniciAdi = kullanici.kullaniciAdi;
                    ViewBag.IsLoggedIn = true;
                    if (kullanici.rol == "Admin")
                    {
                        ViewBag.Role = "Admin";
                    }
                    else ViewBag.Role = "Member";
                }
                var salonlar = _context.BerberSalonları.Select(s => new { s.salonID, s.salonAd }).ToList();
                if (salonlar.Any()) ViewBag.salonlar = salonlar;
            }
            int salonID = Convert.ToInt32(TempData["salonID"]);
            var salon = _context.BerberSalonları.FirstOrDefault(s => s.salonID == salonID);
            ViewBag.salon = salon;
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
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");
            ViewBag.Role = "Admin";
            ViewBag.IsLoggedIn = true;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BerberSalonuEkle(string salonAd, string salonAdres, string salonNumara, string salonEposta, TimeOnly baslangicSaati, TimeOnly bitisSaati)
        {
            var Berbersalonu = new Berbersalonu
            {
                salonAd = salonAd,
                salonAdres = salonAdres,
                salonNumara = salonNumara,
                salonEposta = salonEposta,
                baslangicSaati = baslangicSaati,
                bitisSaati = bitisSaati
            };
            _context.BerberSalonları.Add(Berbersalonu);
            await _context.SaveChangesAsync();
            TempData["msj"] = "Yeni bir berber salonu başarıyla oluşturulmuştur.";
            return RedirectToAction("AdminPaneli");
        }
        [HttpGet]
        public IActionResult SalonPara(int salonID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");
            ViewBag.Role = "Admin";
            ViewBag.IsLoggedIn = true;

            if (salonID <= 0)
            {
                return BadRequest("Geçersiz salon ID.");
            }

            // 1. Uygun tarihler
            var uygunTarihler = _context.UygunTarih
                .Where(t => t.salonID == salonID)
                .Select(t => t.Date.Date)
                .ToList();

            // 2. Personeller
            var personeller = _context.Personeller
                .Where(p => p.salonID == salonID)
                .Select(p => new PersonelViewModel
                {
                    ID = p.personelID,
                    Name = p.isim + " " + p.soyisim
                })
                .ToList();

            // 3. Randevu ücretlerini hesapla
            var randevuBilgileri = _context.Randevular
                .Where(r => r.salonID == salonID)
                .Join(
                    _context.Hizmetler,
                    r => r.hizmetID, // Randevu'daki HizmetID
                    h => h.hizmetID, // Hizmet Tablosundaki ID
                    (r, h) => new
                    {
                        r.personelID,
                        Tarih = r.randevuTarihi.Date,
                        Ucret = h.hizmetFiyat // Hizmet Tablosundan Ücret
                    }
                )
                .GroupBy(r => new { r.personelID, r.Tarih })
                .Select(g => new RandevuBilgi
                {
                    PersonelID = g.Key.personelID,
                    Tarih = g.Key.Tarih,
                    ToplamUcret = g.Sum(r => r.Ucret)
                })
                .ToList();

            var gunlukGelir = _context.Randevular
                .Where(r => r.salonID == salonID)
                .Join(
                    _context.Hizmetler,
                    r => r.hizmetID,
                    h => h.hizmetID,
                    (r, h) => new
                    {
                        Tarih = r.randevuTarihi.Date,
                        Ucret = h.hizmetFiyat
                    }
                )
                    .GroupBy(r => r.Tarih)
                    .Select(g => new GunlukGelirViewModel
                    {
                        Tarih = g.Key,
                        ToplamGelir = g.Sum(x => x.Ucret)
                    })
                    .ToList();

            var genelToplam = gunlukGelir.Sum(g => g.ToplamGelir);
            var viewModel = new SalonParaViewModel
            {
                UygunTarihler = uygunTarihler,
                Personeller = personeller,
                RandevuBilgileri = randevuBilgileri,
                GunlukGelir = gunlukGelir,
                GenelToplam = genelToplam
            };
            ViewData["salonAd"] = _context.BerberSalonları.FirstOrDefault(s => s.salonID == salonID).salonAd;
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult SalonGoster(int salonID)
        {
            var salon = _context.BerberSalonları.FirstOrDefault(s => s.salonID == salonID);
            if (salon != null)  TempData["salonID"] = salonID;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult YapayZeka()
        {
            ViewBag.yapayzeka = true;
            return View("Index");
        }
    }
}
