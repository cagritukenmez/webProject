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
        [HttpGet]
        public IActionResult PersonelOlustur(int salonID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");
            ViewBag.Role = "Admin";
            ViewBag.IsLoggedIn = true;

            ViewBag.salonID = salonID;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PersonelOlustur(int salonID,string isim,string soyisim,string eposta,string telNo)
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
            personel = new Personeller { isim = isim, soyisim= soyisim, eposta = eposta, telNo = telNo,salonID = salonID};
            _context.Personeller.Add(personel);

            await _context.SaveChangesAsync();

            return RedirectToAction("AdminPaneli","BerberSalonu");
        }
        [HttpGet]
        public IActionResult PersonelGoruntule(int salonID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");
            ViewBag.Role = "Admin";
            ViewBag.IsLoggedIn = true;

            var personelListesi = _context.Personeller.Where(p => p.salonID == salonID).ToList();
            ViewBag.salonID = salonID;
            if (personelListesi.Any())
            {
                return View(personelListesi);
            }
            TempData["danger"] = "Herhangi bir personel bulunamadı, lütfen personel ekleyiniz.";
            return RedirectToAction("PersonelOlustur", new { salonID = salonID });
        }
        [HttpGet]
        public IActionResult PersonelDuzenle(int personelID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");
            ViewBag.Role = "Admin";
            ViewBag.IsLoggedIn = true;

            var personel = _context.Personeller.FirstOrDefault(k => k.personelID == Convert.ToInt32(personelID));
            if (personel == null) return NotFound();
            return View(personel);
        }
        [HttpPost]
        public async Task<IActionResult> PersonelDuzenle(int personelID, string isim, string soyisim,string eposta,string telNo)
        {
            var personel = _context.Personeller.FirstOrDefault(k => k.personelID == personelID);
            
            if (personel == null) return NotFound();
            if(personel.eposta != eposta)
            {
                var personelKontrol = _context.Personeller.FirstOrDefault(k=>k.eposta == eposta);
                if(personelKontrol != null)
                {
                    TempData["danger"] = "Bu eposta adresi zaten kullanılmaktadır. Lütfen başka bir eposta adresi seçiniz.";
                    return RedirectToAction("PersonelDuzenle", new { salonID = personel.salonID});
                }
                personel.eposta = eposta;
            }
            if (personel.telNo != telNo)
            {
                var personelKontrol = _context.Personeller.FirstOrDefault(k => k.telNo == telNo);
                if(personelKontrol != null)
                {
                    TempData["danger"] = "Bu telefon numarası zaten kullanılmaktadır. Lütfen başka bir telefon numarası giriniz.";
                    return RedirectToAction("PersonelDuzenle", new { salonID = personel.salonID });
                }
                personel.telNo = telNo;
            }
            _context.SaveChangesAsync();
            TempData["danger"] = "Değişiklikler başarıyla kaydedilmiştir.";
            return RedirectToAction("PersonelGoruntule", new { salonID = personel.salonID });
        }
        [HttpGet]
        public IActionResult PersonelSil(int personelID)
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie == null) return RedirectToAction("Index", "BerberSalonu");
            int kullaniciID = int.Parse(kullaniciIDCookie);
            var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
            if (kullanici == null) return RedirectToAction("Index", "BerberSalonu");
            if (kullanici.rol == "Member") return RedirectToAction("Index", "BerberSalonu");
            ViewBag.Role = "Admin";
            ViewBag.IsLoggedIn = true;

            var personel = _context.Personeller.FirstOrDefault(k => k.personelID == personelID);
            if (personel == null) return NotFound();
            return View(personel);
        }
        [HttpPost]
        public async Task<IActionResult> PersonelSil(int personelID,int evet)
        {
            var personel = await _context.Personeller
                .Include(p => p.Hizmetler) // Hizmetler ilişkisini dahil et
                .Include(p => p.Randevular) // Eğer başka ilişkiler varsa ekleyin
                .FirstOrDefaultAsync(p => p.personelID == personelID);
            if (personel == null)
            {
                TempData["danger"] = "Silinmek istenen personel bulunamadı.";
                return RedirectToAction("Index", "Personel");
            }
            //kontrol et ve sil
            if (personel.Hizmetler != null && personel.Hizmetler.Any())
            {
                _context.Hizmetler.RemoveRange(personel.Hizmetler);
            }
            //kontrol et ve sil
            if (personel.Randevular != null && personel.Randevular.Any())
            {
                _context.Randevular.RemoveRange(personel.Randevular);
            }
            _context.Personeller.Remove(personel);
            // Değişiklikleri kaydet
            await _context.SaveChangesAsync();
            TempData["danger"] = personel.isim + " " + personel.soyisim + " adlı personel başarıyla silindi.";
            return RedirectToAction("PersonelGoruntule", new { salonID = personel.salonID });
        }
    }
}
