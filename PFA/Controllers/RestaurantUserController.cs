using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Text.Json;

namespace PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantsUserController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET tous les restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _context.Restaurant.ToListAsync();
            return Ok(restaurants);
        }

        // ✅ GET recherche
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> SearchRestaurants([FromQuery] string query)
        {
            var results = await _context.Restaurant
                .Where(r => r.Nom.Contains(query) || r.Adresse.Contains(query))
                .ToListAsync();

            return Ok(results);
        }

        // ✅ POST pour stocker un restaurant temporairement en session
        [HttpPost("session")]
        public IActionResult StockerRestaurantEnSession([FromBody] Restaurant resto)
        {
            var json = HttpContext.Session.GetString("restaurants_temp");
            var restaurants = string.IsNullOrEmpty(json)
                ? new List<Restaurant>()
                : JsonSerializer.Deserialize<List<Restaurant>>(json);

            restaurants.Add(resto);
            HttpContext.Session.SetString("restaurants_temp", JsonSerializer.Serialize(restaurants));
            return Ok(new { message = "Ajouté à la session." });
        }


        // ✅ GET pour récupérer la session
        [HttpGet("session")]
        public IActionResult GetRestaurantsEnSession()
        {
            var json = HttpContext.Session.GetString("restaurants_temp");

            if (string.IsNullOrEmpty(json))
                return Ok(new List<Restaurant>());

            var restaurants = JsonSerializer.Deserialize<List<Restaurant>>(json);
            return Ok(restaurants);
        }
    }
}
