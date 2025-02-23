using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFA.Models;
public class Activite
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Nom { get; set; } // Nom de l'activité

    [MaxLength(500)]
    public string Description { get; set; } // Description détaillée de l'activité


    [MaxLength(500)]
    public string Type { get; set; } // type détaillée de l'activité

    [Required]
    public double Prix { get; set; } // Prix en devise locale

    [Required]
    public int Duree { get; set; } // Durée en minutes

    public DateTime DateCreation { get; set; } = DateTime.UtcNow; // Date de création

    [Required]
    public string Emplacement { get; set; } // Adresse ou coordonnées GPS

    public double Evaluation { get; set; } // Note de l'activité (0 à 5 étoiles)

    public int NombreMaxParticipants { get; set; } // Nombre maximum de participants

    public bool EstDisponible { get; set; } = true; // Indique si l'activité est disponible

    [Required]
    public DateTime DateDebut { get; set; } // Date et heure de début de l’activité

    [Required]
    public DateTime DateFin { get; set; } // Date et heure de fin de l’activité

    [Required]
    public string CoordonneesContact { get; set; } // Coordonnées de l'organisateur (email, téléphone)

    [ForeignKey("Destination")]
    public int DestinationId { get; set; }
    public Destination Destination { get; set; } // Lien avec une destination
}
