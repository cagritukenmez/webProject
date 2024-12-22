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
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult HizmetGoruntule(int personelID)
        {
            var personel= _context.Personeller.FirstOrDefault(k => k.personelID == Convert.ToInt32(personelID));
            if (personel == null) return NotFound();
            TempData["msj"] = personel.isim + " " +personel.soyisim;
            var hizmetler = _context.Hizmetler.Include(h => h.personel).Where(h=>h.PersonelID == personelID).ToList();
            return View(hizmetler);
        }
        [HttpPost]
        public IActionResult HizmetDuzenle(int hizmetID)
        {
            var hizmet = _context.Hizmetler.FirstOrDefault(k => k.hizmetID == hizmetID);
            if (hizmet == null) return NotFound();
            return View(hizmet);
        }
        [HttpPost]
        public async Task<IActionResult> HizmetDuzenleAcil(int hizmetID, string hizmetAdi, TimeOnly hizmetSuresi, int hizmetFiyat, int PersonelID)
        {
            var hizmet = _context.Hizmetler.FirstOrDefault(h => h.hizmetID == hizmetID);

            if (hizmet == null)
            {
                TempData["danger"] = "Güncellenmek istenen hizmet bulunamadı.";
                return RedirectToAction("HizmetGoruntule",PersonelID);
            }

            if (hizmet.hizmetAdi != hizmetAdi)
            {
                var hizmetKontrol = _context.Hizmetler.FirstOrDefault(h => h.hizmetAdi == hizmetAdi && h.PersonelID == PersonelID);
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
            return RedirectToAction("HizmetGoruntule",PersonelID);
        }
        [HttpPost]
        public IActionResult HizmetEkle(int personelID)
        {
            var personel = _context.Personeller.FirstOrDefault(k => k.personelID == Convert.ToInt32(personelID));
            if (personel == null) return NotFound();
            TempData["msj"] = personel.isim + " " + personel.soyisim;
            ViewBag.personelID = personelID;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> HizmetEkleAcil(string hizmetAdi,TimeOnly hizmetSuresi,int hizmetFiyat,int personelID)
        {
            var hizmet = _context.Hizmetler.FirstOrDefault(k => k.hizmetAdi == hizmetAdi);
            if (hizmet != null)
            {
                TempData["danger"] = "Böyle bir hizmet zaten bulunmaktadır!!!!";
                ViewBag.personelID = personelID;
                return View("HizmetEkle");
            }
            var personel = await _context.Personeller.FindAsync(personelID);
            if (personel == null)
            {
                TempData["danger"] = "Belirtilen personel bulunamadı!";
                return View("HizmetEkle");
            }

            hizmet = new Hizmetler { hizmetAdi = hizmetAdi, hizmetSuresi = hizmetSuresi, hizmetFiyat = hizmetFiyat, PersonelID = personelID ,personel = personel};
            _context.Hizmetler.Add(hizmet);
            await _context.SaveChangesAsync();
            TempData["danger"] = "Başarılı bir şekilde hizmet eklenmiştir.";
            return RedirectToAction("PersonelGoruntule","Personel", personelID);
        }
    }
}
