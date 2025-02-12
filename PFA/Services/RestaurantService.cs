using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFA.Models;
using PFA.Data;

namespace PFA.Services
{
    public class RestaurantService
    {
        private readonly AppDbContext _context;

        public RestaurantService(AppDbContext context)
        {
            _context = context;
        }

        // 📌 1️⃣ Ajouter un restaurant
        public async Task<Restaurant> AjouterRestaurant(Restaurant restaurant)
        {
            _context.Restaurant.Add(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }

        // 📌 2️⃣ Obtenir la liste des restaurants
        public async Task<List<Restaurant>> ObtenirRestaurants()
        {
            return await _context.Restaurant.Include(r => r.Utilisateur).ToListAsync();
        }

        // 📌 3️⃣ Obtenir un restaurant par ID
        public async Task<Restaurant> ObtenirRestaurantParId(int id)
        {
            return await _context.Restaurant.Include(r => r.Utilisateur)
                                             .FirstOrDefaultAsync(r => r.Id == id);
        }

        // 📌 4️⃣ Mettre à jour un restaurant
        public async Task<bool> MettreAJourRestaurant(int id, Restaurant restaurantMisAJour)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
                return false;

            restaurant.Nom = restaurantMisAJour.Nom;
            restaurant.TypeCuisine = restaurantMisAJour.TypeCuisine;
            restaurant.Adresse = restaurantMisAJour.Adresse;
            restaurant.NumeroTelephone = restaurantMisAJour.NumeroTelephone;

            _context.Restaurant.Update(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }

        // 📌 5️⃣ Supprimer un restaurant
        public async Task<bool> SupprimerRestaurant(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
                return false;

            _context.Restaurant.Remove(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
