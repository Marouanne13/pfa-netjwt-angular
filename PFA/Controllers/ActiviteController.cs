using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiviteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActiviteController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 📌 Récupérer toutes les activités (avec option de filtre)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activite>>> GetActivites([FromQuery] string? nom)
        {
            var query = _context.Activites.Include(a => a.Destination).AsQueryable();

            if (!string.IsNullOrEmpty(nom))
            {
                query = query.Where(a => a.Nom.Contains(nom));
            }

            return await query.ToListAsync();
        }

        // ✅ 📌 Récupérer une activité par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Activite>> GetActivite(int id)
        {
            var activite = await _context.Activites.Include(a => a.Destination).FirstOrDefaultAsync(a => a.Id == id);

            if (activite == null)
            {
                return NotFound(new { message = "Activité non trouvée." });
            }

            return Ok(activite);
        }

        // ✅ 📌 Ajouter une nouvelle activité (éviter les doublons)
        [HttpPost]
        public async Task<ActionResult<Activite>> PostActivite(Activite activite)
        {
            // Vérifie si une activité du même nom existe déjà
            bool existe = await _context.Activites.AnyAsync(a => a.Nom == activite.Nom);
            if (existe)
            {
                return Conflict(new { message = "Une activité avec ce nom existe déjà." });
            }

            _context.Activites.Add(activite);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActivite), new { id = activite.Id }, activite);
        }

        // ✅ 📌 Modifier une activité
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivite(int id, Activite activite)
        {
            if (id != activite.Id)
            {
                return BadRequest(new { message = "L'ID ne correspond pas à l'activité." });
            }

            _context.Entry(activite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Activites.Any(a => a.Id == id))
                {
                    return NotFound(new { message = "Activité non trouvée." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // ✅ 📌 Supprimer une activité
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivite(int id)
        {
            var activite = await _context.Activites.FindAsync(id);
            if (activite == null)
            {
                return NotFound(new { message = "Activité non trouvée." });
            }

            _context.Activites.Remove(activite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
