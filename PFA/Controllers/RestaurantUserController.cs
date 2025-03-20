using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFA.Controllers
{
    [Route("api/restaurantsUser")]
    [ApiController]
    public class RestaurantUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantUserController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 📌 Récupérer les restaurants en fonction de la session de l'utilisateur
        [HttpGet("par-destination")]
        public async Task<IActionResult> GetRestaurantsParDestination()
        {
            // Récupérer l'ID de la destination depuis la session (simulation d'une vraie session)
            int? destinationId = 1; // ⚠️ Remplace par la vraie récupération de session

            if (destinationId == null)
            {
                return BadRequest("Aucune destination trouvée en session.");
            }

            var restaurants = await _context.Restaurant
                .Where(r => r.Adresse.Contains("Casablanca")) // Filtrage basé sur Casablanca
                .ToListAsync();

            if (!restaurants.Any())
            {
                return NotFound($"Aucun restaurant trouvé pour la destination ID {destinationId}");
            }

            return Ok(restaurants);
        }
    }
}
