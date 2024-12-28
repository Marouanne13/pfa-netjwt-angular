using Microsoft.AspNetCore.Mvc;

namespace PFA.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Méthodes
        public void ManageUsers() { /* Logique pour gérer les utilisateurs */ }
        public void ManageActivities() { /* Logique pour gérer les activités */ }
    }
}
