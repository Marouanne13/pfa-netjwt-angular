using Microsoft.EntityFrameworkCore;
using PFA.Models;
using PFA.Data; // Assurez-vous que cet import est présent
namespace PFA.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Panier> Panier { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Activite> Activites { get; set; }
 
        public DbSet<Restaurant> Restaurant { get; set; }



        public DbSet<Hebergement> Hebergements { get; set; }

        public DbSet<Message> Messages { get; set; }
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

                // ✅ Empêcher les doublons d'email
                entity.HasIndex(a => a.Email).IsUnique();

                entity.Property(a => a.DateDeCreation).HasDefaultValueSql("GETDATE()");
                entity.Property(a => a.EstActif).HasDefaultValue(true);
            });
   

            base.OnModelCreating(modelBuilder);

        }

        // 🔹 Configuration de la connexion SQL Server
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=PFA;Trusted_Connection=True;Encrypt=false");
            }
        }
    }
}
