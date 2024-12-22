using System.ComponentModel.DataAnnotations;

namespace myProject.Models
{
    public class Personeller
    {
        [Key]
        public int personelID { get; set; }
        [Required]
        [Display(Name = "İsim")]
        [StringLength(25,MinimumLength =3,ErrorMessage ="Lütfen 3'ten uzun ve 25'dan kısa bir isim giriniz.")]
        public string isim { get; set; }
        [Required]
        [Display(Name = "Soyismi")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Lütfen 3'ten uzun ve 25'dan kısa bir soyisim giriniz.")]
        public string soyisim { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string eposta { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Telefon Numarası")]
        [StringLength(10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Telefon numarası sadece rakamlardan oluşmalıdır.")]
        public string telNo { get; set; }

        [Required]
        [Display(Name = "Personel Hizmetleri")]
        public ICollection<Hizmetler> Hizmetler{ get; set; }
        public ICollection<Randevu> Randevular { get; set; }
        public ICollection<UygunOlmayanTarihSaat> UygunOlmayanTarihSaatler{ get; set; }
    }
}
