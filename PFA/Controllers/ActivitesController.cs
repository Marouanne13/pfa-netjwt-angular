using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PFA.Controllers
{
    [Route("api/activites")]
    [ApiController]
    public class ActiviteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActiviteController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 📌 Récupérer toutes les activités
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activite>>> GetActivites()
        {
            return await _context.Activites.Include(a => a.Destination).ToListAsync();
        }

        // ✅ 📌 Récupérer une activité par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Activite>> GetActivite(int id)
        {
            var activite = await _context.Activites.Include(a => a.Destination).FirstOrDefaultAsync(a => a.Id == id);
            if (activite == null)
            {
                return NotFound(new { Message = "Activité non trouvée" });
            }
            return activite;
        }

// ✅ 📌 Ajouter une activité
[HttpPost]
public async Task<ActionResult<Activite>> CreateActivite([FromBody] Activite activite)
{
    // Vérifier si le modèle est valide
    if (!ModelState.IsValid)
    {
        return BadRequest(new { Message = "Données invalides", Erreurs = ModelState });
    }

    // Vérifier si la destination existe
    var destination = await _context.Destinations.FindAsync(activite.DestinationId);
    if (destination == null)
    {
        return BadRequest(new { Message = "La destination associée n'existe pas." });
    }

    // Vérifier si la date de début est bien avant la date de fin
    if (activite.DateDebut >= activite.DateFin)
    {
        return BadRequest(new { Message = "La date de début doit être antérieure à la date de fin." });
    }

    try
    {
        _context.Activites.Add(activite);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetActivite), new { id = activite.Id }, activite);
    }
    catch (DbUpdateException ex)
    {
        return StatusCode(500, new { Message = "Erreur lors de l'ajout de l'activité.", Erreur = ex.Message });
    }
}


        // ✅ 📌 Modifier une activité
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivite(int id, [FromBody] Activite activite)
        {
            if (id != activite.Id)
            {
                return BadRequest(new { Message = "L'ID de l'activité ne correspond pas." });
            }

            var existingActivite = await _context.Activites.FindAsync(id);
            if (existingActivite == null)
            {
                return NotFound(new { Message = "Activité non trouvée" });
            }

            existingActivite.Nom = activite.Nom;
            existingActivite.Description = activite.Description;
            existingActivite.Type = activite.Type;
            existingActivite.Prix = activite.Prix;
            existingActivite.Duree = activite.Duree;
            existingActivite.Emplacement = activite.Emplacement;
            existingActivite.Evaluation = activite.Evaluation;
            existingActivite.NombreMaxParticipants = activite.NombreMaxParticipants;
            existingActivite.EstDisponible = activite.EstDisponible;
            existingActivite.DateDebut = activite.DateDebut;
            existingActivite.DateFin = activite.DateFin;
            existingActivite.CoordonneesContact = activite.CoordonneesContact;
            existingActivite.DestinationId = activite.DestinationId;

            _context.Entry(existingActivite).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ 📌 Supprimer une activité
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivite(int id)
        {
            var activite = await _context.Activites.FindAsync(id);
            if (activite == null)
            {
                return NotFound(new { Message = "Activité non trouvée" });
            }

            _context.Activites.Remove(activite);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Activité supprimée avec succès." });
        }
    }
}
