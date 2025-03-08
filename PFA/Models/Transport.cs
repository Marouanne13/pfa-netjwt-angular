using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFA.Models
{
    public class Transport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Capacite { get; set; }

        [Required]
        public bool EstDisponible { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeTransport { get; set; }

        [Required]
        public int UtilisateurId { get; set; }

        [ForeignKey("UtilisateurId")]
        public User Utilisateur { get; set; }
        
        [Required]
        public double Prix { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(100)]
        public string NomCompagnie { get; set; }

        [Required]
        [MaxLength(50)]
        public string ModeDeTransport { get; set; }

        public string NumeroImmatriculation { get; set; }

        public TimeSpan HeureDepart { get; set; }

        public TimeSpan HeureArrivee { get; set; }

        public string NumeroServiceClient { get; set; }
    }
}
