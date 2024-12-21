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
            var personel = _context.Personeller.FirstOrDefault(k => k.personelID == Convert.ToInt32(personelID));
            if (personel == null) return NotFound();
            TempData["msj"] = personel.isim + " " +personel.soyisim;
            return View(personel.Hizmetler);
        }
        [HttpPost]
        public IActionResult HizmetDuzenle(int hizmetID)
        {

            return View();
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
            hizmet = new Hizmetler { hizmetAdi = hizmetAdi, hizmetSuresi = hizmetSuresi, hizmetFiyat = hizmetFiyat, PersonelID = personelID };
            _context.Hizmetler.Add(hizmet);
            await _context.SaveChangesAsync();
            TempData["danger"] = "Başarılı bir şekilde hizmet eklenmiştir.";
            return RedirectToAction("PersonelGoruntule","Personel", personelID);
        }
    }
}
