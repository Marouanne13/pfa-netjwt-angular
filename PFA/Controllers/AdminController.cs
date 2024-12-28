using Microsoft.AspNetCore.Mvc;
using PFA.Models;
using PFA.Services;

namespace PFA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult GetAdmins()
        {
            var admins = _adminService.GetAllAdmins();
            return Ok(admins);
        }

        [HttpPost]
        public IActionResult CreateAdmin([FromBody] Admin admin)
        {
            _adminService.CreateAdmin(admin);
            return CreatedAtAction(nameof(GetAdmins), new { id = admin.Id }, admin);
        }
    }
}