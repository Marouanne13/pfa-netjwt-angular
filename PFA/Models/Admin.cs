namespace PFA.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime DateDeCreation { get; set; } = DateTime.UtcNow;
        public DateTime DerniereConnexion { get; set; } = DateTime.UtcNow;
        public bool EstActif { get; set; } = true;
        public string NumeroDeTelephone { get; set; } = string.Empty;
        public string UrlPhotoProfil { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;

        // ❌ Suppression des colonnes obsolètes :
        // public bool PeutGererUtilisateurs { get; set; } = false;
        // public bool PeutGererActivites { get; set; } = false;
        // public bool PeutGererPaiements { get; set; } = false;
        // public bool PeutGererDestinations { get; set; } = false;
    }
}
