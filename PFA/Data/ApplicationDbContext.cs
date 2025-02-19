using Microsoft.EntityFrameworkCore;
using PFA.Models;

namespace PFA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 📌 Empêcher les doublons d'email
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        // 📌 S'assurer que la connexion est bien configurée
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=PFA;Trusted_Connection=True;Encrypt=false");
            }
        }
    }
}
