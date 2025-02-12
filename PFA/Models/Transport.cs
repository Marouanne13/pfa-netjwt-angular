using PFA.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Transport
{
    [Key]
    public int Id { get; set; } // Identifiant unique

    [Required]
    public int Capacite { get; set; } // Capacité maximale du transport (nombre de places disponibles)

    [Required]
    public bool EstDisponible { get; set; } // Indique si le transport est actuellement disponible

    [Required]
    public int TypeTransportId { get; set; } // Clé étrangère vers le type de transport
    public TypeTransport TypeTransport { get; set; } // Relation avec le type de transport

    [Required]
    public int UtilisateurId { get; set; } // Clé étrangère vers l'utilisateur
    public User Utilisateur { get; set; } // Relation avec l'utilisateur

    [Required]
    public double Prix { get; set; } // Prix du transport (en devise locale)

    public DateTime DateCreation { get; set; } = DateTime.UtcNow; // Date de création de l'enregistrement

    // 📌 NOUVEAUX ATTRIBUTS :

    [Required]
    [MaxLength(100)]
    public string NomCompagnie { get; set; } // Nom de la compagnie de transport

    [Required]
    [MaxLength(50)]
    public string ModeDeTransport { get; set; } // Type de transport (ex: "Bus", "Train", "Taxi", "Avion")

    public string NumeroImmatriculation { get; set; } // Numéro d'immatriculation du véhicule

    public TimeSpan HeureDepart { get; set; } // Heure de départ prévue

    public TimeSpan HeureArrivee { get; set; } // Heure d'arrivée prévue
    public string NumeroServiceClient { get; set; } // Numéro de contact pour le service client
}
