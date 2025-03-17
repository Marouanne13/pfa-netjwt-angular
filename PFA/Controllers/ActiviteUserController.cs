using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFA.Controllers
{
    [Route("api/activites")]
    [ApiController]
    public class ActiviteUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActiviteUserController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 📌 Récupérer toutes les activités d'une destination spécifique
        [HttpGet("par-destination/{destinationId}")]
        public async Task<IActionResult> GetActivitesParDestination(int destinationId)
        {
            var activites = await _context.Activites
                .Where(a => a.DestinationId == destinationId) // 🔥 Filtrer par destination
                .ToListAsync();

            if (!activites.Any())
            {
                return NotFound($"Aucune activité trouvée pour la destination ID {destinationId}");
            }

            return Ok(activites);
        }



        // ✅ 📌 Récupérer une activité par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Activite>> GetActiviteById(int id)
        {
            var activite = await _context.Activites.FindAsync(id);
            if (activite == null)
                return NotFound($"Aucune activité trouvée avec l'ID {id}.");

            return Ok(activite);
        }
    }
}
