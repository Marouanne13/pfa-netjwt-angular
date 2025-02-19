using Microsoft.EntityFrameworkCore;
using PFA.Models;

namespace PFA.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Activite> Activites { get; set; }
        public DbSet<TypeActivite> TypeActivite { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }



        public DbSet<Transport> Transports { get; set; }
        public DbSet<TypeTransport> TypeTransports { get; set; }
        public DbSet<Hebergement> Hebergements { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Nom).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Email).IsRequired().HasMaxLength(100);
                entity.Property(a => a.MotDePasse).IsRequired().HasMaxLength(255);
                entity.Property(a => a.Role).HasMaxLength(50);
                entity.Property(a => a.NumeroDeTelephone).HasMaxLength(20);
                entity.Property(a => a.UrlPhotoProfil).HasMaxLength(500);
                entity.Property(a => a.Adresse).HasMaxLength(200);

                // ✅ Supprimer toute référence aux colonnes obsolètes
                // entity.Property(a => a.PeutGererUtilisateurs).HasDefaultValue(false);
                // entity.Property(a => a.PeutGererActivites).HasDefaultValue(false);
                // entity.Property(a => a.PeutGererPaiements).HasDefaultValue(false);
                // entity.Property(a => a.PeutGererDestinations).HasDefaultValue(false);

                entity.Property(a => a.DateDeCreation).HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.EstActif).HasDefaultValue(true);
            });
        }
    }
}
