using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TypeActivite
{
    [Key]
    public int Id { get; set; } // Identifiant unique

    [Required, MaxLength(100)]
    public string Nom { get; set; } // Nom du type d'activité (ex: "Sport", "Culture", "Aventure")

    [MaxLength(500)]
    public string Description { get; set; } // Brève description du type d'activité

    public string Categorie { get; set; } // Catégorie principale (ex: "Tourisme", "Loisirs", "Sport")


}
