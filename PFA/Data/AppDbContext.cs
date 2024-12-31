using Microsoft.EntityFrameworkCore;
using PFA.Models;

namespace PFA.Data
{

    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
       


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(a => a.Id); // Définir la clé primaire
                entity.Property(a => a.Nom).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Email).IsRequired().HasMaxLength(100);
                entity.Property(a => a.MotDePasse).IsRequired().HasMaxLength(255);
                entity.Property(a => a.Role).HasMaxLength(50);
                entity.Property(a => a.NumeroDeTelephone).HasMaxLength(20);
                entity.Property(a => a.UrlPhotoProfil).HasMaxLength(500);
                entity.Property(a => a.Adresse).HasMaxLength(200);

                // Valeurs par défaut
                entity.Property(a => a.DateDeCreation).HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.EstActif).HasDefaultValue(true);
            });
        }
    }

}
