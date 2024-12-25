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
        [ForeignKey("Kullanıcı")]
        public int kullaniciID { get; set; }
        public virtual Kullanıcı kullanici { get; set; }
        [ForeignKey("Personeller")]
        public int personelID { get; set; }
        public virtual Personeller personel { get; set; }
        [ForeignKey("BerberSalonu")]
        public int salonID { get; set; }
        public virtual Berbersalonu Berbersalonu { get; set; }
        [ForeignKey("Hizmetler")]
        public int hizmetID { get; set; }
        public virtual Hizmetler hizmet { get; set; }
    }
}
