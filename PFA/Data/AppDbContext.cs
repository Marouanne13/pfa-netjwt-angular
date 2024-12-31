using Microsoft.EntityFrameworkCore;
using PFA.Models;

namespace PFA.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
    }
}
