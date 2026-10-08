using KonyvtarWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KonyvtarWebApp.Data
{
    public class KonyvtarDbContext : DbContext
    {
        public KonyvtarDbContext(DbContextOptions<KonyvtarDbContext> options) : base(options)
        {
        }

        public DbSet<Konyv> Konyvek { get; set; }
        public DbSet<Szerzo> Szerzok { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the relationship between Konyv and Szerzo
            modelBuilder.Entity<Konyv>()
                .HasOne(k => k.Szerzo)
                .WithMany(s => s.Konyvek)
                .HasForeignKey(k => k.SzerzoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
