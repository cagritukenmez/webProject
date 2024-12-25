using Microsoft.AspNetCore.Mvc;
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
        public IActionResult randevduAl()
        {
            var kullaniciIDCookie = Request.Cookies["KullaniciID"];
            if (kullaniciIDCookie != null)
            {
                int kullaniciID = int.Parse(kullaniciIDCookie);
                var kullanici = _context.Kullanıcı.FirstOrDefault(k => k.kullaniciID == kullaniciID);
                if (kullanici != null)
                {
                    return View(kullanici);
                }

            }
            return RedirectToAction("Index");
        }

    }
}
