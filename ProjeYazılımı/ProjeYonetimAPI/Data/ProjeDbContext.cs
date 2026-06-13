using Microsoft.EntityFrameworkCore;
using ProjeYonetimAPI.Models;

namespace ProjeYonetimAPI.Data
{
    public class ProjeDbContext : DbContext
    {
        public ProjeDbContext(DbContextOptions<ProjeDbContext> options) : base(options) { }

        public DbSet<EkipUyesi> EkipUyeleri { get; set; }
        public DbSet<Sprint> Sprintler { get; set; }
        public DbSet<SprintGorev> SprintGorevler { get; set; }
        public DbSet<HaftalikRapor> HaftalikRaporlar { get; set; }
        public DbSet<BütçeKalemi> BütçeKalemleri { get; set; }
        public DbSet<ProjeGorev> ProjeGorevler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SprintGorev>()
                .HasOne(sg => sg.Sprint)
                .WithMany(s => s.Gorevler)
                .HasForeignKey(sg => sg.SprintId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EkipUyesi>().Property(e => e.AylikUcret).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<BütçeKalemi>().Property(b => b.Tutar).HasColumnType("decimal(18,2)");
        }
    }
}
