using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myProject.Models;

namespace myProject.Controllers
{
    public class RandevuController : Controller
    {
        private readonly myyDbContext _context;
        public RandevuController(myyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult RandevduAl()
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie != null)
            {
                int kullaniciID = int.Parse(kullaniciIDCookie);
                var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
                if (kullanici != null)
                {
                    ViewBag.kullanici = kullanici;
                    return View();
                }

            }
            return RedirectToAction("Index","BerberSalonu");
        }
        [HttpPost]
        public async Task<IActionResult> RandevuAl(DateTime randevuTarihi, TimeOnly randevuSaati, int personelID, int salonID, int hizmetID)
        {
            var tarihKontrol = _context.UygunTarih.FirstOrDefault(tk => tk.salonID == salonID && tk.Date.Date == randevuTarihi.Date);
            if (tarihKontrol == null) return RedirectToAction("Index","BerberSalonu");
            if(tarihKontrol.IsAvailable == false) {// tarih kontrol tablosunda bu salona ait bir gün eğer kapalı değilse bugün kapalı hatası döndür
                TempData["msj"] = "Bu Gün Kapalı!.";
                return RedirectToAction("RandevuAl");
            }
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            var kontrol = _context.Randevular.Where(k => k.randevuTarihi == randevuTarihi && k.randevuSaati == randevuSaati && k.salonID == salonID);
            if (kontrol.Any())
            {
                TempData["msj"] = "Seçtiğiniz saatte zaten bir randevu bulunmaktadır. ";
                return RedirectToAction("RandevuAl");
            }
            if (DateTime.Now.Date > randevuTarihi.Date)
            {
                TempData["msj"] = "Geçmiş bir günden randevu alınamaz!!!";
                return RedirectToAction("RandevuAl");
            }

            var randevum = new Randevu
            {
                randevuTarihi = randevuTarihi,
                randevuSaati = randevuSaati,
                kullaniciID = Convert.ToInt32(kullaniciIDCookie),
                personelID = personelID,
                salonID = salonID,
                hizmetID = hizmetID,
                onaylimi = false
            };
            _context.Randevular.Add(randevum);
            await _context.SaveChangesAsync();


            return RedirectToAction("RandevuGoruntule");
        }
        [HttpGet]
        public IActionResult RandevuGoruntule()
        {

            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie != null)
            {
                int kullaniciID = int.Parse(kullaniciIDCookie);
                var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
                ViewBag.Role = kullanici.rol;
                if(kullanici != null)
                {
                    var randevular = _context.Randevular
                        .Where(r => r.kullaniciID == kullaniciID)
                        .Include(r => r.Berbersalonu)
                        .Include(r => r.personel)
                        .Include(r => r.hizmet)
                        .ToList();
                    if(kullanici.rol == "Admin")
                    {
                        ViewBag.Role = "Admin";
                        ViewBag.IsLoggedIn = true;
                        randevular = _context.Randevular
                            .Include(r => r.Berbersalonu)
                            .Include(r => r.personel)
                            .Include(r => r.hizmet)
                            .ToList();
                    }
                    return View(randevular);
                }
            }
            return View("RandevuAl");  
        }
        [HttpPost]
        public IActionResult RandevuOnay(int randevuID)
        {


            var randevu = _context.Randevular.FirstOrDefault(r => r.randevuID == randevuID);
            if(randevu != null)
            {
                randevu.onaylimi = true;
                _context.Randevular.Update(randevu);
                _context.SaveChanges();
                return RedirectToAction("RandevuGoruntule2", new { salonID = randevu.salonID });
            }
            return RedirectToAction("AdminPaneli", "BerberSalonu");
        }

        [HttpGet]
        public IActionResult RandevuGoruntule2(int salonID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");
            ViewBag.Role = "Admin";
            ViewBag.IsLoggedIn = true;

            var randevular = _context.Randevular.Where(r => r.salonID == salonID)
                .Include(r => r.personel)
                .Include(r => r.Berbersalonu)
                .Include(r => r.hizmet)
                .ToList();
            if (randevular.Any())
            {
                return View(randevular);
            }
            return RedirectToAction("AdminPaneli","BerberSalonu");
        }

        // AJAX: GetPersoneller
        [HttpPost]
        public IActionResult GetPersoneller(int salonID)
        {
            var personeller = _context.Personeller
                .Where(p => p.salonID == salonID)
                .Select(p => new SelectListItem
                {
                    Value = p.personelID.ToString(),
                    Text = p.isim.ToUpper() + " " + p.soyisim.ToUpper()
                })
                .ToList();

            return Json(personeller);
        }

        // AJAX: GetHizmetler
        [HttpPost]
        public IActionResult GetHizmetler(int personelID)
        {
            var hizmetler = _context.Hizmetler
                .Where(h => h.personelID == personelID)
                .Select(h => new SelectListItem
                {
                    Value = h.hizmetID.ToString(),
                    Text = h.hizmetAdi.ToUpper() + " " + h.hizmetFiyat.ToString() + "TL"
                })
                .ToList();

            return Json(hizmetler);
        }

        // AJAX: GetUygunTarihler
        [HttpPost]
        public IActionResult GetUygunTarihler(int salonID, int personelID)
        {
            // Tüm tarihler ve saatler için kontrol edilen liste
            var mevcutRandevular = _context.Randevular
                .Where(r => r.salonID == salonID && r.personelID == personelID)
                .Select(r => new { r.randevuTarihi, r.randevuSaati })
                .ToList();

            var uygunTarihler = new List<DateTime>();
            var baslangicTarihi = DateTime.Today;
            var bitisTarihi = baslangicTarihi.AddMonths(1); // 1 ay sonrasına kadar göster

            for (var tarih = baslangicTarihi; tarih <= bitisTarihi; tarih = tarih.AddDays(1))
            {
                // Her gün için kontrol et
                if (mevcutRandevular.Any(r => r.randevuTarihi.Date == tarih.Date))
                {
                    continue; // Zaten doluysa ekleme
                }

                uygunTarihler.Add(tarih);
            }

            return Json(uygunTarihler.Select(t => t.ToString("yyyy-MM-dd"))); // Takvim için format
        }

        [HttpPost]
        public IActionResult GetSalon()
        {
            var salonlar = _context.BerberSalonları
                .Select(s => new SelectListItem
                {
                    Value = s.salonID.ToString(),
                    Text = s.salonAd
                })
                .ToList();

            return Json(salonlar);
        }
    }


}
