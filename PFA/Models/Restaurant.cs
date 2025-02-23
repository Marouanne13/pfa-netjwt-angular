using PFA.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Restaurant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ✅ Permet à SQL Server de gérer l'ID automatiquement
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Nom { get; set; }

    [Required, MaxLength(100)]
    public string TypeCuisine { get; set; }

    [Required]
    public int? UtilisateurId { get; set; }

    public User? Utilisateur { get; set; }

    [Required, MaxLength(250)]
    public string Adresse { get; set; }

    public string NumeroTelephone { get; set; }

    public bool LivraisonDisponible { get; set; }

    public bool ReservationEnLigne { get; set; }

    public bool EstOuvert24h { get; set; }

    public int NombreEtoiles { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public string ImageUrl { get; set; }
}
