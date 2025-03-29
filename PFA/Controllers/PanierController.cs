using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PFA.Controllers
{
    [Route("api/panier")]
    [ApiController]
    public class PanierUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PanierUserController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Récupérer le panier complet d’un utilisateur avec les données liées
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPanier(int userId)
        {
            var panier = await _context.Panier
                .Where(p => p.UserId == userId)
                .Include(p => p.Destination)
                .Include(p => p.Hebergement)
                .Include(p => p.Activite)
                .Include(p => p.Transport)
                .Include(p => p.Restaurant)
                .ToListAsync();

            if (!panier.Any())
                return NotFound("Le panier est vide.");

            return Ok(panier);
        }

        // ✅ Ajouter une ligne au panier
        [HttpPost("ajouter")]
        public IActionResult AjouterPanier([FromBody] Panier panier)
        {
            if (panier.UserId == 0 || panier.DestinationId == 0)
                return BadRequest("UserId ou DestinationId manquant");

            _context.Panier.Add(panier);
            _context.SaveChanges();

            return Ok(new { message = "Panier ajouté avec succès !" });
        }

        // ✅ Supprimer une ligne de panier
        [HttpDelete("supprimer/{id}")]
        public async Task<IActionResult> SupprimerDuPanier(int id)
        {
            var panier = await _context.Panier.FindAsync(id);
            if (panier == null)
                return NotFound("Élément introuvable.");

            _context.Panier.Remove(panier);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Élément supprimé du panier !" });
        }

        // ✅ Vider tout le panier d’un utilisateur
        [HttpDelete("vider/{userId}")]
        public async Task<IActionResult> ViderPanier(int userId)
        {
            var paniers = _context.Panier.Where(p => p.UserId == userId);
            if (!paniers.Any())
                return NotFound("Aucun élément à supprimer.");

            _context.Panier.RemoveRange(paniers);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Panier vidé avec succès !" });
        }

        // ✅ Calculer le total du dernier panier valide uniquement
        [HttpGet("total/{userId}")]
        public async Task<IActionResult> CalculerTotalPanier(int userId)
        {
            var dernierPanier = await _context.Panier
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Id)
                .Include(p => p.Hebergement)
                .Include(p => p.Activite)
                .Include(p => p.Transport)
                .FirstOrDefaultAsync();

            if (dernierPanier == null)
                return Ok(0);

            double total = 0;

            if (dernierPanier.Hebergement != null)
                total += dernierPanier.Hebergement.PrixParNuit;

            if (dernierPanier.Activite != null)
                total += dernierPanier.Activite.Prix;

            if (dernierPanier.Transport != null)
                total += dernierPanier.Transport.Prix;

            return Ok(total);
        }
    }
}
