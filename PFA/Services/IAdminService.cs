using PFA.Models;

namespace PFA.Services
{
    public interface IAdminService
    {
        IEnumerable<Admin> GetAllAdmins();
        Task<Admin> CreateAdmin(Admin admin); // 🔹 Ajouter un admin
    }

}
