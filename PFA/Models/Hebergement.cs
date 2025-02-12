using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Hebergement
{
    [Key]
    public int Id { get; set; } // Identifiant unique

    [Required, MaxLength(150)]
    public string Nom { get; set; } // Nom de l’hébergement (ex: "Hôtel Atlas", "Villa Luxe")

    [Required, MaxLength(100)]
    public string Type { get; set; } // Type d’hébergement ("Hôtel", "Appartement", "Airbnb", "Villa")

    [Required, MaxLength(250)]
    public string Adresse { get; set; } // Adresse complète

    [Required]
    public double PrixParNuit { get; set; } // Prix par nuit en devise locale

    public DateTime DateCreation { get; set; } = DateTime.UtcNow; // Date de création

    // 📌 NOUVEAUX ATTRIBUTS :

    public int ClassementEtoiles { get; set; } // Classement en étoiles (1 à 5 étoiles)
    public bool PetitDejeunerInclus { get; set; } // Indique si le petit-déjeuner est inclus

    public bool Piscine { get; set; } // Indique si une piscine est disponible
    public string NumeroTelephone { get; set; } // Numéro de contact

}
