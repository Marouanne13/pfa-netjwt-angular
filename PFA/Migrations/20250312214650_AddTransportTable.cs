using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA.Migrations
{
    public partial class AddTransportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacite = table.Column<int>(nullable: false),
                    EstDisponible = table.Column<bool>(nullable: false),
                    TypeTransport = table.Column<string>(maxLength: 100, nullable: false),
                    Prix = table.Column<double>(nullable: false),
                    DateCreation = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    NomCompagnie = table.Column<string>(maxLength: 100, nullable: false),
                    ModeDeTransport = table.Column<string>(maxLength: 50, nullable: false),
                    NumeroImmatriculation = table.Column<string>(nullable: true),
                    HeureDepart = table.Column<TimeSpan>(nullable: false),
                    HeureArrivee = table.Column<TimeSpan>(nullable: false),
                    NumeroServiceClient = table.Column<string>(nullable: true),
                    DestinationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transports_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transports_DestinationId",
                table: "Transports",
                column: "DestinationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transports");
        }
    }
}
