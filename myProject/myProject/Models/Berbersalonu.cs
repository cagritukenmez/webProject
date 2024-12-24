using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myProject.Models
{
    public class Berbersalonu
    {
        [Key]
        public int salonID { get; set; }
        [Required]
        [Display(Name = "Salon İsmi")]
        [StringLength(35, MinimumLength = 5, ErrorMessage = "Lütfen 3'ten uzun ve 20'dan kısa bir salon ismi giriniz.")]
        public string salonAd { get; set; }
        [Required]
        [Display(Name = "Salon Adresi")]
        [StringLength(50, MinimumLength = 20, ErrorMessage = "Lütfen 20'ten uzun ve 50'dan kısa bir salon adresi giriniz.")]
        public string salonAdres { get; set; }
        [Required]
        [Display(Name = "Salon Telefon Numarası")]
        [Phone]
        [StringLength(10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Telefon numarası sadece rakamlardan oluşmalıdır.")]
        public string salonNumara { get; set; }
        [Required]
        [Display(Name = "Salon Epostası")]
        [EmailAddress]
        public string salonEposta { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Salonun Açılma Saati")]
        public TimeOnly baslangicSaati { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Salonun Kapanma Saati")]
        public TimeOnly bitisSaati { get; set; }
        public ICollection<Hizmetler> Hizmetler { get; set; }
        public ICollection<Randevu> Randevular { get; set; }
        public ICollection<Personeller> Personeller { get; set; }
        public ICollection<UygunTarih> UygunTarih { get; set; }
    }
}
