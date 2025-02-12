using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PFA.Models;
using PFA.Services;

namespace PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // 📌 1️⃣ Obtenir tous les restaurants
        [HttpGet]
        public async Task<ActionResult<List<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _restaurantService.ObtenirRestaurants();
            return Ok(restaurants);
        }

        // 📌 2️⃣ Obtenir un restaurant par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.ObtenirRestaurantParId(id);
            if (restaurant == null)
                return NotFound("Restaurant introuvable.");

            return Ok(restaurant);
        }

        // 📌 3️⃣ Ajouter un restaurant
        [HttpPost]
        public async Task<ActionResult<Restaurant>> AjouterRestaurant(Restaurant restaurant)
        {
            var nouveauRestaurant = await _restaurantService.AjouterRestaurant(restaurant);
            return CreatedAtAction(nameof(GetRestaurant), new { id = nouveauRestaurant.Id }, nouveauRestaurant);
        }

        // 📌 4️⃣ Mettre à jour un restaurant
        [HttpPut("{id}")]
        public async Task<IActionResult> MettreAJourRestaurant(int id, Restaurant restaurant)
        {
            var result = await _restaurantService.MettreAJourRestaurant(id, restaurant);
            if (!result)
                return NotFound("Restaurant introuvable.");

            return NoContent();
        }

        // 📌 5️⃣ Supprimer un restaurant
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
