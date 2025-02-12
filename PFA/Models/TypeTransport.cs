using System.ComponentModel.DataAnnotations;

public class TypeTransport
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }
}
