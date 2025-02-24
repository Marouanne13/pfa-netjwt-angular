using Microsoft.EntityFrameworkCore;
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
            if (restaurant.UtilisateurId == null)
            {
                throw new ArgumentException("Le champ UtilisateurId est requis.");
            }

            restaurant.Id = 0; // Assurez-vous que l'ID n'est pas défini manuellement.
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
        // 📌 4️⃣ Mettre à jour un restaurant
        public async Task<bool> MettreAJourRestaurant(int id, Restaurant restaurantMisAJour)
        {
            if (id != restaurantMisAJour.Id)
            {
                throw new ArgumentException("L'ID fourni ne correspond pas à celui du restaurant.");
            }

            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
                return false;

            restaurant.Nom = restaurantMisAJour.Nom;
            restaurant.TypeCuisine = restaurantMisAJour.TypeCuisine;
            restaurant.Adresse = restaurantMisAJour.Adresse;
            restaurant.NumeroTelephone = restaurantMisAJour.NumeroTelephone;
            restaurant.NombreEtoiles = restaurantMisAJour.NombreEtoiles;
            restaurant.Description = restaurantMisAJour.Description;
            restaurant.ImageUrl = restaurantMisAJour.ImageUrl;
            restaurant.LivraisonDisponible = restaurantMisAJour.LivraisonDisponible;
            restaurant.ReservationEnLigne = restaurantMisAJour.ReservationEnLigne;
            restaurant.EstOuvert24h = restaurantMisAJour.EstOuvert24h;

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
