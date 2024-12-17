using System.ComponentModel.DataAnnotations;

namespace myProject.Models
{
    public class UygunOlmayanTarihSaat
    {
        [Key]
        public int tarihSaatId { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name ="Uygun olmayan randevu tarihi")]
        public DateTime randevuTarihi { get; set; }
        [DataType(DataType.Time)]
        [Required]
        [Display(Name = "Uygun olmayan randevu saati")]
        public TimeSpan randevuSaati { get; set; }
    }
}
