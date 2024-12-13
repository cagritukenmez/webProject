using System.ComponentModel.DataAnnotations;

namespace myProject.Models
{
    public class Personeller
    {
        [Key]
        public int personelID { get; set; }
        [Required]
        [Display(Name = "Personel İsmi")]
        [StringLength(25,MinimumLength =3,ErrorMessage ="Lütfen 3'ten uzun ve 25'dan kısa bir isim giriniz.")]
        public string isim { get; set; }
        [Required]
        [Display(Name = "Personel Soyismi")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Lütfen 3'ten uzun ve 25'dan kısa bir soyisim giriniz.")]
        public string soyisim { get; set; }

        [Required]
        [Display(Name = "Personel Hizmetleri")]
        public List<Hizmetler> hizmetler { get; set; }
    }
}
