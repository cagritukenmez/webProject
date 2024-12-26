using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using myProject.Models;

namespace myProject.Controllers
{
    public class UygunTarihController : Controller
    {
        private readonly myyDbContext _context;
        public UygunTarihController(myyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult UygunTarihAc(int salonID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index","BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");

            var salon = _context.BerberSalonları.FirstOrDefault(s => s.salonID == salonID);
            if(salon != null)
            {
                ViewBag.salon = salon;
                return View();
            }
            return RedirectToAction("AdminPaneli","BerberSalonu");
        }
        [HttpPost]
        public async Task<IActionResult> UygunTarihAc(int salonID, DateTime date)
        {
            // Bugünden itibaren belirtilen tarihe kadar olan tüm günler
            DateTime today = DateTime.Today;
            List<UygunTarih> uygunTarihListesi = new List<UygunTarih>();
            for (DateTime current = today; current <= date; current = current.AddDays(1))
            {
                current = current.Date;
                bool mevcut = _context.UygunTarih.Any(ut => ut.Date == current);
                if (!mevcut)
                {
                    uygunTarihListesi.Add(new UygunTarih
                    {
                        salonID = salonID,
                        Date = current,
                        IsAvailable = true
                    });
                }
            }
            if (uygunTarihListesi.Count > 0)
            {
                _context.UygunTarih.AddRange(uygunTarihListesi);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("AdminPaneli", "BerberSalonu");
        }
        [HttpGet]
        public IActionResult UygunTarihGoruntule(int salonID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");

            var salon = _context.BerberSalonları.FirstOrDefault(s => s.salonID == salonID);
            if(salon == null) return RedirectToAction("AdminPaneli", "BerberSalonu");
            ViewBag.salonAd = salon.salonAd;
            var uygunTarihListe = _context.UygunTarih.Where(ut => ut.salonID == salonID).ToList();
            if (uygunTarihListe.Any())
            {
                return View(uygunTarihListe);
            }
            return RedirectToAction("AdminPaneli","BerberSalonu");
        }
        [HttpPost]
        public IActionResult UygunTarihDegistir(int salonID,DateTime Date)
        {
            var UygunTarih = _context.UygunTarih.FirstOrDefault(ut => ut.salonID == salonID && ut.Date == Date);
            if(UygunTarih != null)
            {
                UygunTarih.IsAvailable = !(UygunTarih.IsAvailable);
                _context.UygunTarih.Update(UygunTarih);
                _context.SaveChanges();
            }
            return RedirectToAction("UygunTarihGoruntule", new { salonID = salonID });
        }
    }
}
