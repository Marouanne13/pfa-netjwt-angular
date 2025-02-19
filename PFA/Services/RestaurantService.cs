using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;

namespace PFA.Services
{
    public class RestaurantService
    {
        private readonly AppDbContext _context;

        public RestaurantService(AppDbContext context)
        {
            _context = context;
        }

        // 📌 Ajouter un restaurant
        public async Task<Restaurant> AjouterRestaurant(Restaurant restaurant)
        {
            if (restaurant == null) return null;

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }

        // 📌 Obtenir tous les restaurants
        public async Task<List<Restaurant>> ObtenirRestaurants()
        {
            return await _context.Restaurants
                .Include(r => r.Utilisateur)
                .ToListAsync();
        }

        // 📌 Obtenir un restaurant par ID
        public async Task<Restaurant> ObtenirRestaurantParId(int id)
        {
            return await _context.Restaurants
                .Include(r => r.Utilisateur)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // 📌 Modifier un restaurant
        public async Task<bool> ModifierRestaurant(int id, Restaurant restaurantMisAJour)
        {
            if (restaurantMisAJour == null) return false;

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;

            restaurant.Nom = restaurantMisAJour.Nom;
            restaurant.TypeCuisine = restaurantMisAJour.TypeCuisine;
            restaurant.Adresse = restaurantMisAJour.Adresse;
            restaurant.NumeroTelephone = restaurantMisAJour.NumeroTelephone;
            restaurant.LivraisonDisponible = restaurantMisAJour.LivraisonDisponible;
            restaurant.ReservationEnLigne = restaurantMisAJour.ReservationEnLigne;
            restaurant.EstOuvert24h = restaurantMisAJour.EstOuvert24h;
            restaurant.NombreEtoiles = restaurantMisAJour.NombreEtoiles;
            restaurant.Description = restaurantMisAJour.Description;
            restaurant.ImageUrl = restaurantMisAJour.ImageUrl;

            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }

        // 📌 Supprimer un restaurant
        public async Task<bool> SupprimerRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
