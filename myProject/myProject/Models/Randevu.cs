using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myProject.Models
{
    public class Randevu
    {
        [Key]
        public int randevuID { get; set; }
        [Display(Name = "Randevu Tarihi")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime randevuTarihi { get; set; }
        [DataType(DataType.Time)]
        [Required]
        [Display(Name = "Randevu Saati")]
        public TimeSpan randevuSaati { get; set; }
        [Required]
        [Display(Name = "Personel Seçiniz.")]
        public Personeller personel { get; set; }
        [Required]
        [Display(Name = "Hizmet Seçiniz.")]
        public Hizmetler hizmet { get; set; }
        [ForeignKey]
        public int musteriId { get; set; }
    }
}
