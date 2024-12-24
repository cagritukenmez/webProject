using Microsoft.EntityFrameworkCore;

namespace myProject.Models
{
    public class myyDbContext:DbContext
    {
        public myyDbContext(DbContextOptions<myyDbContext> options) : base(options)
        {
        }
        public DbSet<Berbersalonu> BerberSalonları { get; set; }
        public DbSet<Hizmetler> Hizmetler { get; set; }
        public DbSet<Personeller> Personeller { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Kullanıcı> Kullanıcı { get; set; }
        public DbSet<UygunTarih> UygunTarih { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server =(localdb)\mssqllocaldb;
Database=BerberSalonuDB;Trusted_Connection=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Hizmetler ve Personeller arasındaki ilişki
            modelBuilder.Entity<Hizmetler>()
                .HasOne(h => h.Personel) // Hizmetlerin bir Personel ile ilişkisi var
                .WithMany(p => p.Hizmetler) // Personelin birden fazla Hizmeti var
                .HasForeignKey(h => h.personelID) // personelID, foreign key olarak tanımlanıyor
                .OnDelete(DeleteBehavior.Restrict); // Silme davranışı: Restrict

            // Hizmetler ve BerberSalonları arasındaki ilişki
            modelBuilder.Entity<Hizmetler>()
                .HasOne(h => h.Berbersalonu) // Hizmetlerin bir Berber Salonu ile ilişkisi var
                .WithMany(b => b.Hizmetler) // Bir Berber Salonunda birden fazla Hizmet olabilir
                .HasForeignKey(h => h.salonID) // berberSalonID, foreign key olarak tanımlanıyor
                .OnDelete(DeleteBehavior.Restrict); // Silme davranışı: Restrict

            // Randevular ve BerberSalonları arasındaki ilişki
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Berbersalonu) // Randevular bir Berber Salonu ile ilişkili
                .WithMany(b => b.Randevular) // Bir Berber Salonunda birden fazla Randevu olabilir
                .HasForeignKey(r => r.salonID) // berberSalonID, foreign key olarak tanımlanıyor
                .OnDelete(DeleteBehavior.Restrict);

            // Personeller ve BerberSalonları arasındaki ilişki
            modelBuilder.Entity<Personeller>()
                .HasOne(p => p.Berbersalonu) // Personeller bir Berber Salonu ile ilişkili
                .WithMany(b => b.Personeller) // Bir Berber Salonunda birden fazla Personel olabilir
                .HasForeignKey(p => p.salonID) // berberSalonID, foreign key olarak tanımlanıyor
                .OnDelete(DeleteBehavior.Restrict);

            // UygunTarihler ve BerberSalonları arasındaki ilişki
            modelBuilder.Entity<UygunTarih>()
                .HasOne(u => u.Berbersalonu) // Uygun Tarihler bir Berber Salonu ile ilişkili
                .WithMany(b => b.UygunTarih) // Bir Berber Salonunda birden fazla Uygun Tarih olabilir
                .HasForeignKey(u => u.salonID) // berberSalonID, foreign key olarak tanımlanıyor
                .OnDelete(DeleteBehavior.Restrict);

            // Randevular ve Kullanıcılar arasındaki ilişki
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.kullanici) // Randevular bir Kullanıcı ile ilişkili
                .WithMany(k => k.Randevular) // Bir Kullanıcıda birden fazla Randevu olabilir
                .HasForeignKey(r => r.kullaniciID) // kullanıcıID, foreign key olarak tanımlanıyor
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
