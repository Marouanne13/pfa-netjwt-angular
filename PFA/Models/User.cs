namespace PFA.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Nom { get; set; } // Nom complet
        public string Email { get; set; } // Adresse e-mail
        public string MotDePasse { get; set; } // Mot de passe
        public string Telephone { get; set; } // Numéro de téléphone
        public string Adresse { get; set; } // Adresse
        public DateTime DateDeNaissance { get; set; } // Date de naissance de l'utilisateur
        public string Genre { get; set; } // Genre de l'utilisateur ("Homme", "Femme", "Autre")
        public DateTime DateDeCreation { get; set; } // Date de création du compte
        public bool EstActif { get; set; } // Statut d'activation du compte 
        public ICollection<Admin> Admins { get; set; } // Relation avec les administrateurs, si nécessaire
    }

}
