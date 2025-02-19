using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;

namespace PFA.Services
{
    public class AdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            _context = context;
        }

        // 📌 Vérifier si l'admin a le bon rôle
        public async Task<bool> HasRole(int adminId, string requiredRole)
        {
            var admin = await _context.Admins.FindAsync(adminId);
            if (admin == null)
                return false;

            return admin.Role == requiredRole || admin.Role == "SuperAdmin"; // "SuperAdmin" a tous les droits
        }
    }
}
