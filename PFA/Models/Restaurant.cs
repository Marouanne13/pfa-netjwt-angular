using PFA.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Restaurant
{
    [Key]
    public int Id { get; set; } // Identifiant unique

    [Required, MaxLength(150)]
    public string Nom { get; set; } // Nom du restaurant

    [Required, MaxLength(100)]
    public string TypeCuisine { get; set; } // Type de cuisine (ex: "Italienne", "Marocaine", "Japonaise")

    [Required]
    public int? UtilisateurId { get; set; } // Clé étrangère vers l'utilisateur qui gère le restaurant
    public User? Utilisateur { get; set; } // Relation avec l'utilisateur

    // 📌 NOUVEAUX ATTRIBUTS :

    [Required, MaxLength(250)]
    public string Adresse { get; set; } = string.Empty; // Adresse complète du restaurant

    public string NumeroTelephone { get; set; } = string.Empty; // Numéro de contact

    public bool LivraisonDisponible { get; set; }  // Indique si le restaurant propose la livraison

    public bool ReservationEnLigne { get; set; } // Indique si les réservations en ligne sont disponibles

    public bool EstOuvert24h { get; set; } // Indique si le restaurant est ouvert 24h/24
    public int NombreEtoiles { get; set; } // Note moyenne du restaurant (1 à 5 étoiles)

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty; // Brève description du restaurant

    public string ImageUrl { get; set; } = string.Empty; // URL de l’image principale du restaurant
}
