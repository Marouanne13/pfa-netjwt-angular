using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Linq;

namespace PFA.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Admin> GetAllAdmins() => _context.Admins.ToList();

        public void CreateAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }
    }
}
