using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA.Migrations
{
    public partial class AddTableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Créer la table Admins
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateDeCreation = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DerniereConnexion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstActif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    NumeroDeTelephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UrlPhotoProfil = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PeutGererUtilisateurs = table.Column<bool>(type: "bit", nullable: false),
                    PeutGererActivites = table.Column<bool>(type: "bit", nullable: false),
                    PeutGererPaiements = table.Column<bool>(type: "bit", nullable: false),
                    PeutGererDestinations = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            // Insérer les données dans la table Admins
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Nom", "Email", "MotDePasse", "NumeroDeTelephone", "Adresse", "DateDeCreation", "DerniereConnexion", "EstActif", "Role" },
                values: new object[,]
                {
                    { 1, "Mahdi", "Mahdi123@gmail.com", "123456789", "0612345678", "Adresse Admin 1", DateTime.Now, DateTime.Now, true, "Gérer les voyages" },
                    { 2, "Emjad", "Emjad123@gmail.com", "123456789", "0623456789", "Adresse Admin 2", DateTime.Now, DateTime.Now, true, "Gérer les activités" },
                    { 3, "Amine", "Amine123@gmail.com", "123456789", "0634567890", "Adresse Admin 3", DateTime.Now, DateTime.Now, true, "Gérer les paiements" },
                    { 4, "Asmae", "Asmae123@gmail.com", "123456789", "0645678901", "Adresse Admin 4", DateTime.Now, DateTime.Now, true, "Gérer les restaurants" },
                    { 5, "Marouane", "maroaune123@gmail.com", "hashedpassword5", "0656789012", "Adresse Admin 5", DateTime.Now, DateTime.Now, true, "Gérer les clients" }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Supprimer les administrateurs ajoutés
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 }
            );

            // Supprimer la table Admins
            migrationBuilder.DropTable(
                name: "Admins");
        }
    }
}
