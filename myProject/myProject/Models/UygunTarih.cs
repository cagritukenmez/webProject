using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace myProject.Models
{
    public class UygunTarih
    {
        [Key]
        public int tarihID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }
        [ForeignKey("BerberSalonu")]
        public int salonID { get; set; }
        public virtual Berbersalonu Berbersalonu { get; set; }
    }
}
