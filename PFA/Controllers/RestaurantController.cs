using Microsoft.AspNetCore.Mvc;
using PFA.Services;
using PFA.Data; // Si nécessaire
using Microsoft.EntityFrameworkCore;


namespace PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController()
        {
            // ⚠️ Initialisation manuelle sans injection de dépendance
            _restaurantService = new RestaurantService(new AppDbContext(new DbContextOptions<AppDbContext>()));
        }

        [HttpGet]
        public async Task<ActionResult<List<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _restaurantService.ObtenirRestaurants();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.ObtenirRestaurantParId(id);
            if (restaurant == null)
                return NotFound("Restaurant introuvable.");

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> AjouterRestaurant([FromBody] Restaurant restaurant)
        {
            if (restaurant == null)
                return BadRequest("Les données du restaurant sont invalides.");

            if (string.IsNullOrEmpty(restaurant.Nom) || string.IsNullOrEmpty(restaurant.Adresse))
                return BadRequest("Le nom et l'adresse sont obligatoires.");

            if (restaurant.UtilisateurId == null)
                return BadRequest("L'utilisateur est obligatoire.");

            var nouveauRestaurant = await _restaurantService.AjouterRestaurant(restaurant);
            return CreatedAtAction(nameof(GetRestaurant), new { id = nouveauRestaurant.Id }, nouveauRestaurant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> MettreAJourRestaurant(int id, [FromBody] Restaurant restaurant)
        {
            if (id != restaurant.Id)
                return BadRequest("L'ID fourni ne correspond pas à celui du restaurant.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _restaurantService.MettreAJourRestaurant(id, restaurant);
            if (!result)
                return NotFound("Restaurant introuvable.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SupprimerRestaurant(int id)
        {
            var result = await _restaurantService.SupprimerRestaurant(id);
            if (!result)
                return NotFound("Restaurant introuvable.");

            return NoContent();
        }
    }
}
