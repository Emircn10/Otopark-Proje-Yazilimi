using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtoparkSistemi.Models;

namespace OtoparkSistemi.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Arac> Araclar { get; set; }
        public DbSet<Abonelik> Abonelikler { get; set; }
        public DbSet<GirisKayit> GirisKayitlari { get; set; }
        public DbSet<Tarife> Tarifeler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Arac>()
                .HasIndex(a => a.Plaka)
                .IsUnique();

            modelBuilder.Entity<GirisKayit>()
                .HasOne(g => g.Arac)
                .WithMany(a => a.GirisKayitlari)
                .HasForeignKey(g => g.AracId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Abonelik>()
                .HasOne(a => a.Arac)
                .WithMany(ar => ar.Abonelikler)
                .HasForeignKey(a => a.AracId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
