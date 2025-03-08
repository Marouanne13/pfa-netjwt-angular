using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PFA.Models
{
    public class Hebergement

    {
        [Key]
        public int Id { get; set; } // Identifiant unique

        [Required, MaxLength(150)]
        public string Nom { get; set; }

        [Required, MaxLength(100)]
        public string Type { get; set; }

        [Required, MaxLength(250)]
        public string Adresse { get; set; }

        [Required]
        public double PrixParNuit { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        // 📌 NOUVEAUX ATTRIBUTS :

        public int ClassementEtoiles { get; set; }
        public bool PetitDejeunerInclus { get; set; }

        public bool Piscine { get; set; }
        public string NumeroTelephone { get; set; }
        public int DestinationId { get; set; }

        [JsonIgnore]
        public virtual Destination Destination { get; set; }

    }
}
