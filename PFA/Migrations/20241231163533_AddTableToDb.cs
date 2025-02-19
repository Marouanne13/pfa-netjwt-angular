using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA.Migrations
{
    public partial class AddTableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
           name: "Admins",
           columns: table => new
           {
               Id = table.Column<int>(type: "int", nullable: false)
                   .Annotation("SqlServer:Identity", "1, 1"),
               Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
               Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
               MotDePasse = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
               IdUtilisateur = table.Column<int>(type: "int", nullable: false, defaultValue: 0), // ✅ Ajout d'une valeur par défaut
               Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
               DateDeCreation = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
               DerniereConnexion = table.Column<DateTime>(type: "datetime2", nullable: false),
               EstActif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
               NumeroDeTelephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
               UrlPhotoProfil = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
               Adresse = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
           },
           constraints: table =>
           {
               table.PrimaryKey("PK_Admins", x => x.Id);
           });

            // 📌 Insérer les données dans la table Admins (⚠️ Suppression des colonnes inutiles)
            migrationBuilder.InsertData(
     table: "Admins",
     columns: new[] { "Id", "Nom", "Email", "MotDePasse", "NumeroDeTelephone", "Adresse", "DateDeCreation", "DerniereConnexion", "EstActif", "Role", "UrlPhotoProfil", "IdUtilisateur" },
     values: new object[,]
     {
        { 1, "Mahdi", "Mahdi123@gmail.com", "123456789", "0612345678", "Adresse Admin 1", DateTime.UtcNow, DateTime.UtcNow, true, "Gérer les voyages", "default.jpg", 1 },
        { 2, "Emjad", "Emjad123@gmail.com", "123456789", "0623456789", "Adresse Admin 2", DateTime.UtcNow, DateTime.UtcNow, true, "Gérer les activités", "default.jpg", 2 },
        { 3, "Amine", "Amine123@gmail.com", "123456789", "0634567890", "Adresse Admin 3", DateTime.UtcNow, DateTime.UtcNow, true, "Gérer les paiements", "default.jpg", 3 },
        { 4, "Asmae", "Asmae123@gmail.com", "123456789", "0645678901", "Adresse Admin 4", DateTime.UtcNow, DateTime.UtcNow, true, "Gérer les restaurants", "default.jpg", 4 },
        { 5, "Marouane", "maroaune123@gmail.com", "hashedpassword5", "0656789012", "Adresse Admin 5", DateTime.UtcNow, DateTime.UtcNow, true, "Gérer les clients", "default.jpg", 5 }
     }
 );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 📌 Supprimer les administrateurs ajoutés
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 }
            );

            // 📌 Supprimer la table Admins
            migrationBuilder.DropTable(
                name: "Admins");
        }
    }
}
