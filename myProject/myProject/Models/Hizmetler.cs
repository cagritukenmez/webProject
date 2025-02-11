﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myProject.Models
{
    public class Hizmetler
    {
        [Key]
        public int hizmetID { get; set; }
        [Required]
        [Display(Name = "Hizmetin Adı")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Lütfen 3'ten uzun ve 20'dan kısa bir hizmet ismi giriniz.")]
        public string hizmetAdi { get; set; }
        [Required]
        [Display(Name = "Hizmetin Suresi")]
        public TimeOnly hizmetSuresi { get; set; }
        [Required]
        [Display(Name = "Hizmetin Fiyatı")]
        public int hizmetFiyat { get; set; }
        [ForeignKey("Personeller")]
        public int personelID { get; set; }
        public virtual Personeller Personel { get; set; }
        [ForeignKey("BerberSalonu")]
        public int salonID { get; set; }
        public virtual Berbersalonu Berbersalonu { get; set; }

    }
}
