namespace PFA.Models
{
    public class Destination
    {
        public int Id { get; set; } // Identifiant unique

        public string Nom { get; set; } = string.Empty; // Nom de la destination (par exemple : Casablanca, Marrakech)

        public string Description { get; set; } = string.Empty; // Description de la destination

        public string Region { get; set; } = string.Empty;// Région de la ville au Maroc (ex : Casablanca-Settat, Marrakech-Safi)

        public double Latitude { get; set; }  // Coordonnée Latitude

        public double Longitude { get; set; } // Coordonnée Longitude

        public string ImageUrl { get; set; } = string.Empty; // URL de l'image représentant la destination

        public bool EstPopulaire { get; set; } // Indicateur si la destination est populaire (true/false)

        public int NombreVisites { get; set; } // Nombre de fois que la destination a été visitée

        // Validation pour garantir que la ville appartient au Maroc
        public string Ville
        {
            get => _ville;
            set
            {
                var villesMarocaines = new List<string>
            {
                "Casablanca", "Marrakech", "Rabat", "Fès", "Tanger", "Agadir",
                "Oujda", "Meknès", "Tetouan", "Safi", "El Jadida", "Essaouira",
                "Nador", "Béni Mellal", "Saidia" ,"ifrane"
            };

                if (!villesMarocaines.Contains(value))
                {
                    throw new ArgumentException("La ville doit être une ville marocaine.");
                }
                _ville = value;
            }
        }
        private string _ville;
    }

}
