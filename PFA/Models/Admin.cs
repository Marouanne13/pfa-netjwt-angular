namespace PFA.Models
{
    public class Admin
    {
        // Identifiant unique de l'administrateur
        public int Id { get; set; }
        public string Nom { get; set; } // Nom de l'administrateur
        public string Email { get; set; } // Adresse email
        public string MotDePasse { get; set; } // Mot de passe
        public int IdUtilisateur { get; set; } // Identifiant de l'utilisateur associé
        public string Role { get; set; } // Exemple : "SuperAdmin", "Manager", "Éditeur"
        public DateTime DateDeCreation { get; set; } // Date de création du compte
        public DateTime DerniereConnexion { get; set; } // Date de la dernière connexion
        public bool EstActif { get; set; } // Statut d'activation de l'administrateur
        public string NumeroDeTelephone { get; set; } // Numéro de téléphone
        public string UrlPhotoProfil { get; set; } // URL de la photo de profil
        public string Adresse { get; set; } // Adresse de l'administrateur
        public bool PeutGererUtilisateurs { get; set; } // Peut gérer les utilisateurs
        public bool PeutGererActivites { get; set; } // Peut gérer les activités
        public bool PeutGererPaiements { get; set; } // Peut gérer les paiements
        public bool PeutGererDestinations { get; set; } // Peut gérer les destinations
    }

}
