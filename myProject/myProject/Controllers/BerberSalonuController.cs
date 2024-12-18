using Microsoft.AspNetCore.Mvc;
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
                        return RedirectToAction("AdminPaneli", "BerberSalonu");
                    }
                }


            }
            return View();
        }
        public IActionResult AdminPaneli()
        {
            //ekleme düzenleme.
            return View();
        }
    }
}
