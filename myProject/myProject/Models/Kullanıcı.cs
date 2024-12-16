using System.ComponentModel.DataAnnotations;
namespace myProject.Models
{
    public class Kullanıcı
    {
        [Key]
        public int kullaniciID { get; set; }
        [Required]
        [StringLength(15,MinimumLength =3,ErrorMessage ="Şifre en az 3 karakter uzunluğunda olmalıdır.")]
        [Display(Name ="Kullanıcı Adı :")]
        public string kullaniciAdi { get; set; }
        [Required]
        [StringLength(15,MinimumLength =8,ErrorMessage = "Şifre en az 8 karakter uzunluğunda olmalıdır.")]
        [Display(Name ="Şifre :")]
        public string kullaniciSifre { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Eposta :")]
        public string eposta { get; set; }
        public string rol { get; set; }
    }
}
