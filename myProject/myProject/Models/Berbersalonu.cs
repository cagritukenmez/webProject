using System.ComponentModel.DataAnnotations;

namespace myProject.Models
{
    public class Berbersalonu
    {
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
        public int salonNumara { get; set; }
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
        //public List<Hizmetler> hizmetler { get; set; }
        public List<Personeller> personeller { get; set; }
    }
}
