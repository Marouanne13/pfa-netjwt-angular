using PFA.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Panier
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    public int? DestinationId { get; set; }
    public int? HebergementId { get; set; }
    public int? ActiviteId { get; set; }
    public int? TransportId { get; set; }
    public int? RestaurantId { get; set; }

    public User User { get; set; }
    public Destination Destination { get; set; }
    public Hebergement Hebergements { get; set; }
    public Activite Activite { get; set; }
    public Transport Transport { get; set; }
    public Restaurant Restaurant { get; set; }
}
