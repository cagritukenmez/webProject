namespace myProject.Models
{
    public class SalonParaViewModel
    {
        public List<DateTime> UygunTarihler { get; set; } // Tablonun üst kısmında yer alan tarihler
        public List<PersonelViewModel> Personeller { get; set; } // Tablonun sol kısmındaki personeller
        public List<RandevuBilgi> RandevuBilgileri { get; set; } // Randevuların ücret bilgisi
        public List<GunlukGelirViewModel> GunlukGelir { get; set; }
        public decimal GenelToplam { get; set; }
    }

    public class PersonelViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class RandevuBilgi
    {
        public int PersonelID { get; set; }
        public DateTime Tarih { get; set; }
        public decimal ToplamUcret { get; set; }
    }
}
