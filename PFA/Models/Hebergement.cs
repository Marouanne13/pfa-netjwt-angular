using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PFA.Models
{
    [Table("Hebergements")] // ✅ Assure que le nom de la table est correct en base
    public class Hebergement
    {
        [Key]
        public int Id { get; set; } // Identifiant unique

        private string _nom = string.Empty;
        private string _type = string.Empty;
        private string _adresse = string.Empty;
        private string? _numeroTelephone;

        [Required, MaxLength(150)]
        public required string Nom
        {
            get => _nom;
            set => _nom = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(Nom), "Le nom ne peut pas être vide.") : value;
        }

        [Required, MaxLength(100)]
        public required string Type
        {
            get => _type;
            set => _type = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(Type), "Le type ne peut pas être vide.") : value;
        }

        [Required, MaxLength(250)]
        public required string Adresse
        {
            get => _adresse;
            set => _adresse = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(Adresse), "L'adresse ne peut pas être vide.") : value;
        }

        [Required]
        public double PrixParNuit { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        // 📌 NOUVEAUX ATTRIBUTS :
        public int ClassementEtoiles { get; set; }
        public bool PetitDejeunerInclus { get; set; }
        public bool Piscine { get; set; }

        [MaxLength(20)]
        public string? NumeroTelephone
        {
            get => _numeroTelephone;
            set => _numeroTelephone = string.IsNullOrWhiteSpace(value) ? null : value; // ✅ Convertir les chaînes vides en null
        }

        // Clé étrangère pour Destination
        [Required]
        [ForeignKey("Destination")]
        public int DestinationId { get; set; }

        [JsonIgnore]
        public virtual Destination? Destination { get; set; } // ✅ Ajout de "?" pour éviter les erreurs nullable
    }
}
