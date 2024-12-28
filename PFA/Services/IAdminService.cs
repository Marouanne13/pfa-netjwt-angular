using PFA.Models;

namespace PFA.Services
{
    public interface IAdminService
    {
        IEnumerable<Admin> GetAllAdmins();
        void CreateAdmin(Admin admin);
    }

}
