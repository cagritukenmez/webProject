using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myProject.Models;

namespace myProject.Controllers
{
    public class PersonelController : Controller
    {
        private readonly myyDbContext _context;
        public PersonelController(myyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PersonelOlustur()
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PersonelOlustur(string isim,string soyisim,string eposta,string telNo)
        {
            var personel = _context.Personeller.FirstOrDefault(k => k.eposta == eposta);
            if (personel != null)
            {
                TempData["msj"] = "Bu eposta adresi zaten kullanılıyor.";
                return RedirectToAction("PersonelOlustur");
            }
            personel = _context.Personeller.FirstOrDefault(k => k.telNo == telNo);
            if (personel != null)
            {
                TempData["msj"] = "Bu telefon numarası zaten kullanılıyor.";
                return RedirectToAction("PersonelOlustur");
            }
            personel = new Personeller { isim = isim, soyisim= soyisim, eposta = eposta, telNo = telNo};
            _context.Personeller.Add(personel);

            await _context.SaveChangesAsync();

            return RedirectToAction("AdminPaneli","BerberSalonu");
        }
        [HttpGet]
        public IActionResult PersonelGoruntule()
        {
            var personelListesi = _context.Personeller.ToList();
            return View(personelListesi);
        }
        [HttpPost]
        public IActionResult PersonelDuzenle(int personelID)
        {
            var personel = _context.Personeller.FirstOrDefault(k => k.personelID == Convert.ToInt32(personelID));
            if (personel == null) return NotFound();
            return View(personel);
        }
        [HttpPost]
        public async Task<IActionResult> PersonelDuzenleAcil(int personelID, string isim, string soyisim,string eposta,string telNo )
        {
            var personel = _context.Personeller.FirstOrDefault(k => k.personelID == personelID);
            
            if (personel == null) return NotFound();
            if(personel.eposta != eposta)
            {
                var personelKontrol = _context.Personeller.FirstOrDefault(k=>k.eposta == eposta);
                if(personelKontrol != null)
                {
                    TempData["msj"] = "Bu eposta adresi zaten kullanılmaktadır. Lütfen başka bir eposta adresi seçiniz.";
                    return RedirectToAction("PersonelDuzenle", personelID);
                }
                personel.eposta = eposta;
            }
            if (personel.telNo != telNo)
            {
                var personelKontrol = _context.Personeller.FirstOrDefault(k => k.telNo == telNo);
                if(personelKontrol != null)
                {
                    TempData["msj"] = "Bu telefon numarası zaten kullanılmaktadır. Lütfen başka bir telefon numarası giriniz.";
                    return RedirectToAction("PersonelDuzenle", personelID);
                }
                personel.telNo = telNo;
            }
            _context.SaveChangesAsync();
            TempData["msj"] = "Değişiklikler başarıyla kaydedilmiştir.";
            return RedirectToAction("PersonelGoruntule");
        }
        public IActionResult PersonelSil()
        {
            return View();
        }
    }
}
