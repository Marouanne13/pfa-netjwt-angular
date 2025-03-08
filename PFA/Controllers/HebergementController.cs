using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using PFA.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFA.Controllers
{
    [Route("api/hebergements")]
    [ApiController]
    public class HebergementController : ControllerBase
    {
        private readonly HebergementService _hebergementService;
        private readonly AppDbContext _context;

        public HebergementController(HebergementService hebergementService, AppDbContext context)
        {
            _hebergementService = hebergementService;
            _context = context;
        }

        // ✅ 📌 Récupérer tous les hébergements
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Hebergement>>> GetAllHebergements()
        {
            var hebergements = await _context.Hebergements.ToListAsync();
            return Ok(hebergements);
        }

        // ✅ 📌 Récupérer les hébergements d'une destination spécifique
        [HttpGet("destination/{nom}")]
        public async Task<ActionResult<IEnumerable<Hebergement>>> GetHebergementsParDestination(string nom)
        {
            var hebergements = await _context.Hebergements
                .Include(h => h.Destination) // 🔥 Assure d'inclure la relation Destination
                .Where(h => h.Destination.Nom == nom)
                .ToListAsync();

            if (hebergements == null || !hebergements.Any())
                return NotFound($"Aucun hébergement trouvé pour la destination : {nom}");

            return Ok(hebergements);
        }

        // ✅ 📌 Ajouter un hébergement
        [HttpPost("ajouter")]
        public async Task<ActionResult<Hebergement>> AjouterHebergement([FromBody] Hebergement hebergement)
        {
            if (hebergement == null)
                return BadRequest("Les données de l'hébergement sont invalides.");

            // ✅ Vérifier si la destination existe avant d'ajouter l'hébergement
            var destinationExiste = await _context.Destinations.FindAsync(hebergement.DestinationId);
            if (destinationExiste == null)
                return BadRequest($"La destination avec ID {hebergement.DestinationId} n'existe pas.");

            // ✅ Forcer Destination à NULL pour éviter la validation forcée
            hebergement.Destination = null;

            _context.Hebergements.Add(hebergement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllHebergements), new { id = hebergement.Id }, hebergement);
        }



        // ✅ 📌 Modifier un hébergement
        [HttpPut("modifier/{id}")]
        public async Task<IActionResult> ModifierHebergement(int id, [FromBody] Hebergement hebergement)
        {
            if (id != hebergement.Id)
                return BadRequest("L'ID de l'hébergement ne correspond pas.");

            var hebergementExiste = await _context.Hebergements.FindAsync(id);
            if (hebergementExiste == null)
                return NotFound("Hébergement non trouvé.");

            _context.Entry(hebergementExiste).CurrentValues.SetValues(hebergement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ 📌 Supprimer un hébergement
        [HttpDelete("supprimer/{id}")]
        public async Task<IActionResult> SupprimerHebergement(int id)
        {
            var hebergement = await _context.Hebergements.FindAsync(id);
            if (hebergement == null)
                return NotFound("Hébergement non trouvé.");

            _context.Hebergements.Remove(hebergement);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
