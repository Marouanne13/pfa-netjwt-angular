namespace PFA.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ExpediteurId { get; set; } // ID de l'expéditeur (User ou Admin)
        public int DestinataireId { get; set; } // ID du destinataire (User ou Admin)
        public string ExpediteurType { get; set; } = "User"; // "User" ou "Admin"
        public string Contenu { get; set; } = string.Empty;
        public DateTime DateEnvoi { get; set; } = DateTime.UtcNow;
        public bool EstLu { get; set; } = false;
    }
}
