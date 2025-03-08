using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PFA.Migrations
{
    public partial class AddHebergementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hebergements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Adresse = table.Column<string>(nullable: false),
                    PrixParNuit = table.Column<double>(nullable: false),
                    DateCreation = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ClassementEtoiles = table.Column<int>(nullable: false),
                    PetitDejeunerInclus = table.Column<bool>(nullable: false),
                    Piscine = table.Column<bool>(nullable: false),
                    NumeroTelephone = table.Column<string>(nullable: true),
                    DestinationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hebergements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hebergements_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hebergements_DestinationId",
                table: "Hebergements",
                column: "DestinationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Hebergements");
        }
    }
}
