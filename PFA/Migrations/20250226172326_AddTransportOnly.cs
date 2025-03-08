using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA.Migrations
{
    /// <inheritdoc />
    public partial class AddTransportOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacite = table.Column<int>(type: "int", nullable: false),
                    EstDisponible = table.Column<bool>(type: "bit", nullable: false),
                    TypeTransport = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomCompagnie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModeDeTransport = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroImmatriculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeureDepart = table.Column<TimeSpan>(type: "time", nullable: false),
                    HeureArrivee = table.Column<TimeSpan>(type: "time", nullable: false),
                    NumeroServiceClient = table.Column<string>(type: "nvarchar(max)", nullable: true),


                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transports_Users_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transports_UtilisateurId",
                table: "Transports",
                column: "UtilisateurId");

        }
    }
}