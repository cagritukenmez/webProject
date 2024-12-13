using System.ComponentModel.DataAnnotations;

namespace myProject.Models
{
    public class Randevu
    {
        [Key]
        public int randevuID { get; set; }
        [Required]
        [Display(Name = "Telefon Numarası")]
        [Phone]
        public int musteriNumara { get; set; }
        [Display(Name = "Randevu Tarihi")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime randevuTarihi { get; set; }
        [DataType(DataType.Time)]
        [Required]
        [Display(Name = "Randevu Saati")]
        public TimeSpan randevuSaati { get; set; }
        [Required]
        [Display(Name = "İsim")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Lütfen 3'ten uzun ve 25'dan kısa bir isim giriniz.")]
        public string musteriAd { get; set; }
        [Required]
        [Display(Name = "Soyisim")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Lütfen 3'ten uzun ve 25'dan kısa bir soyisim giriniz.")]
        public string musteriSoyad { get; set; }
        [Required]
        [Display(Name = "Personel Seçiniz.")]
        public Personeller personel { get; set; }
        [Required]
        [Display(Name = "Hizmet Seçiniz.")]
        public Hizmetler hizmet { get; set; }
    }
}
