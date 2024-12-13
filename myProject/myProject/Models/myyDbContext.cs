using Microsoft.EntityFrameworkCore;

namespace myProject.Models
{
    public class myyDbContext:DbContext
    {
        public DbSet<Berbersalonu> BerberSalonları { get; set; }
        public DbSet<Hizmetler> Hizmetler { get; set; }
        public DbSet<Personeller> Personeller { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server =(localdb)\mssqllocaldb;
Database=BerberSalonuDB;Trusted_Connection=True;");
        }
    }
}
