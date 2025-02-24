using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace PFA.Migrations
{
    /// <inheritdoc />
    public partial class Activites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Type = table.Column<string>(maxLength: 500, nullable: true),
                    Prix = table.Column<double>(nullable: false),
                    Duree = table.Column<int>(nullable: false),
                    DateCreation = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Emplacement = table.Column<string>(nullable: false),
                    Evaluation = table.Column<double>(nullable: true),
                    NombreMaxParticipants = table.Column<int>(nullable: true),
                    EstDisponible = table.Column<bool>(nullable: false, defaultValue: true),
                    DateDebut = table.Column<DateTime>(nullable: false),
                    DateFin = table.Column<DateTime>(nullable: false),
                    CoordonneesContact = table.Column<string>(nullable: false),
                    DestinationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activites_Destination_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activites_DestinationId",
                table: "Activites",
                column: "DestinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Activites");
        }
    }
}
