using PFA.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Panier
{
    [Key]
    public int Id { get; set; }

    // 🔹 Clé étrangère vers User
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("Destination")]
    public int? DestinationId { get; set; }

    [ForeignKey("Hebergement")]
    public int? HebergementId { get; set; }

    [ForeignKey("Activite")]
    public int? ActiviteId { get; set; }

    [ForeignKey("Transport")]
    public int? TransportId { get; set; }

    [ForeignKey("Restaurant")]
    public int? RestaurantId { get; set; }

    // 🔹 Propriétés de navigation (Relations avec les autres tables)
    [JsonIgnore]
    public virtual User? User { get; set; }

    [JsonIgnore]
    public virtual Destination? Destination { get; set; }

    [JsonIgnore]
    public virtual Hebergement? Hebergement { get; set; } // Ajout de virtual

    [JsonIgnore]
    public virtual Activite? Activite { get; set; }

    [JsonIgnore]
    public virtual Transport? Transport { get; set; }

    [JsonIgnore]
    public virtual Restaurant? Restaurant { get; set; }
}
