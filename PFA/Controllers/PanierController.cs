using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System;
using System.Collections.Generic;
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

        // ✅ 📌 Récupérer le panier d'un utilisateur par UserId
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

        // ✅ 📌 Ajouter un élément au panier
        [HttpPost("ajouter")]
        public async Task<IActionResult> AjouterAuPanier([FromBody] Panier panier)
        {
            if (panier == null)
            {
                return BadRequest("❌ Les données du panier sont nulles.");
            }

            try
            {
                _context.Panier.Add(panier);
                await _context.SaveChangesAsync();
                return Ok(new { message = "✅ Panier ajouté avec succès" });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Erreur SQL: {dbEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur: {ex.Message}");
            }
        }



        // ✅ 📌 Supprimer un élément du panier
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

        // ✅ 📌 Vider complètement le panier
        [HttpDelete("vider/{userId}")]
        public async Task<IActionResult> ViderPanier(int userId)
        {
            var panier = _context.Panier.Where(p => p.UserId == userId);
            if (!panier.Any())
                return NotFound("Aucun élément à supprimer.");

            _context.Panier.RemoveRange(panier);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Panier vidé avec succès !" });
        }
    }
}
