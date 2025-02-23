using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA.Migrations
{
    /// <inheritdoc />
    public partial class Restaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Vérifier si la table "Restaurants" existe avant de la supprimer
            if (migrationBuilder.ActiveProvider == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                migrationBuilder.Sql(@"
                    IF OBJECT_ID('dbo.Restaurants', 'U') IS NOT NULL 
                    DROP TABLE Restaurants;
                ");
            }

            // Création de la table "Restaurant"
            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TypeCuisine = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NumeroTelephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LivraisonDisponible = table.Column<bool>(type: "bit", nullable: false),
                    ReservationEnLigne = table.Column<bool>(type: "bit", nullable: false),
                    EstOuvert24h = table.Column<bool>(type: "bit", nullable: false),
                    NombreEtoiles = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurant_Users_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_UtilisateurId",
                table: "Restaurant",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Suppression de la table "Restaurant"
            migrationBuilder.DropTable(
                name: "Restaurant");

            // Recréation de l'ancienne table "Restaurants" en cas de rollback
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EstOuvert24h = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LivraisonDisponible = table.Column<bool>(type: "bit", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NombreEtoiles = table.Column<int>(type: "int", nullable: false),
                    NumeroTelephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationEnLigne = table.Column<bool>(type: "bit", nullable: false),
                    TypeCuisine = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Users_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_UtilisateurId",
                table: "Restaurants",
                column: "UtilisateurId");
        }
    }
}
