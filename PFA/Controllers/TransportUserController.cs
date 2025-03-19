using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFA.Controllers
{
    [Route("api/transport-user")]
    [ApiController]
    public class TransportUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransportUserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("destination/{destinationId}")]
        public IActionResult GetTransportsByDestination(int destinationId)
        {
            var transports = _context.Transports
                .Where(t => t.DestinationId == destinationId)
                .ToList();

            if (!transports.Any())
            {
                return NotFound("Aucun transport trouvé pour cette destination.");
            }

            return Ok(transports);
        }
    }

}