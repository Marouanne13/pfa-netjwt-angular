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

        // ✅ 📌 Récupérer toutes les activités disponibles
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Activite>>> GetAllActivites()
        {
            var activites = await _context.Activites.ToListAsync();
            if (activites == null || !activites.Any())
                return NotFound("Aucune activité disponible.");

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

        // ✅ 📌 Récupérer les activités d'une destination spécifique
        [HttpGet("destination/{destinationId}")]
        public async Task<ActionResult<IEnumerable<Activite>>> GetActivitesByDestination(int destinationId)
        {
            var activites = await _context.Activites
                .Where(a => a.DestinationId == destinationId)
                .ToListAsync();

            if (activites == null || !activites.Any())
                return NotFound($"Aucune activité trouvée pour la destination ID {destinationId}.");

            return Ok(activites);
        }
    }
}
