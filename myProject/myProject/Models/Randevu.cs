using System.ComponentModel.DataAnnotations;

namespace myProject.Models
{
    public class Randevu
    {
        [Key]
        public int randevuID { get; set; }
        [Required]
        [Phone]
        public int musteriNumara { get; set; }
        public DateTime randevuTarihi { get; set; }
        public string musteriAd { get; set; }
        public string musteriSoyad { get; set; }
        public int personelID { get; set; }
    }
}
