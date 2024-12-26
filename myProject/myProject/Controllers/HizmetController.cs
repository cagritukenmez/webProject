using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myProject.Models;

namespace myProject.Controllers
{
    public class HizmetController : Controller
    {
        private readonly myyDbContext _context;
        public HizmetController(myyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult HizmetGoruntule(int personelID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");

            var personel= _context.Personeller.FirstOrDefault(k => k.personelID == personelID);
            if (personel == null) return RedirectToAction("AdminPaneli","BerberSalonu");
            TempData["msj"] = personel.isim + " " +personel.soyisim;
            var hizmetler = _context.Hizmetler.Where(h=>h.personelID == personelID).ToList();
            if (hizmetler.Any())
            {
                return View(hizmetler);
            }
            TempData["danger"] = "Herhangi bir hizmet bulunamadı, hizmet ekleyiniz.";
            return RedirectToAction("HizmetEkle", new { personelID = personelID });
        }
        [HttpGet]
        public IActionResult HizmetDuzenle(int hizmetID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");

            var hizmet = _context.Hizmetler.FirstOrDefault(k => k.hizmetID == hizmetID);
            if (hizmet == null) return NotFound();
            return View(hizmet);
        }
        [HttpPost]
        public async Task<IActionResult> HizmetDuzenle(int hizmetID, string hizmetAdi, TimeOnly hizmetSuresi, int hizmetFiyat)
        {
            var hizmet = _context.Hizmetler.FirstOrDefault(h => h.hizmetID == hizmetID);
            if (hizmet == null)
            {
                TempData["danger"] = "Güncellenmek istenen hizmet bulunamadı.";
                return RedirectToAction("HizmetGoruntule", hizmet.personelID);
            }

            if (hizmet.hizmetAdi != hizmetAdi)
            {
                var hizmetKontrol = _context.Hizmetler.FirstOrDefault(h => h.hizmetAdi == hizmetAdi);
                if (hizmetKontrol != null)
                {
                    TempData["danger"] = "Bu isimde bir hizmet zaten bulunmaktadır. Lütfen başka bir isim seçiniz.";
                    return RedirectToAction("HizmetDuzenle", new { hizmetID });
                }
                hizmet.hizmetAdi = hizmetAdi;
            }

            if (hizmet.hizmetSuresi != hizmetSuresi)
            {
                hizmet.hizmetSuresi = hizmetSuresi;
            }

            if (hizmet.hizmetFiyat != hizmetFiyat)
            {
                hizmet.hizmetFiyat = hizmetFiyat;
            }
            await _context.SaveChangesAsync();
            TempData["danger"] = "Hizmet başarıyla güncellenmiştir.";
            return RedirectToAction("HizmetGoruntule", new { personelID=hizmet.personelID});
        }
        [HttpGet]
        public IActionResult HizmetEkle(int personelID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");

            var personel = _context.Personeller.FirstOrDefault(k => k.personelID == personelID);
            if (personel == null) return NotFound();
            TempData["msj"] = personel.isim + " " + personel.soyisim;
            ViewBag.personelID = personelID;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> HizmetEkle(string hizmetAdi, TimeOnly hizmetSuresi, int hizmetFiyat, int personelID)
        {
            var hizmet = _context.Hizmetler.FirstOrDefault(k => k.hizmetAdi == hizmetAdi);
            if (hizmet != null)
            {
                TempData["danger"] = "Böyle bir hizmet zaten bulunmaktadır!!!!";
                ViewBag.personelID = personelID;
                return View("HizmetEkle");
            }
            var personel = _context.Personeller.FirstOrDefault(k => k.personelID == personelID);
            if (personel == null)
            {
                TempData["danger"] = "Belirtilen personel bulunamadı!";
                return View("HizmetEkle");
            }

            hizmet = new Hizmetler { hizmetAdi = hizmetAdi, hizmetSuresi = hizmetSuresi, hizmetFiyat = hizmetFiyat, personelID = personelID, salonID = personel.salonID };
            _context.Hizmetler.Add(hizmet);
            await _context.SaveChangesAsync();
            TempData["danger"] = "Başarılı bir şekilde hizmet eklenmiştir.";
            return RedirectToAction("HizmetGoruntule", "Hizmet", new { personelID = personelID });
        }
        [HttpGet]
        public IActionResult HizmetSil(int hizmetID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");

            var hizmet = _context.Hizmetler.FirstOrDefault(k => k.hizmetID == hizmetID);
            return View(hizmet);
        }
        [HttpPost]
        public async Task<IActionResult> HizmetSil(int hizmetID,int evet)
        {
            var hizmet = _context.Hizmetler.FirstOrDefault(k => k.hizmetID == hizmetID);
            _context.Hizmetler.Remove(hizmet);
            // Değişiklikleri kaydet
            await _context.SaveChangesAsync();
            TempData["danger"] = "Hizmet başarıyla silindi.";
            return RedirectToAction("HizmetGoruntule", new { personelID= hizmet.personelID});
        }
    }
}
