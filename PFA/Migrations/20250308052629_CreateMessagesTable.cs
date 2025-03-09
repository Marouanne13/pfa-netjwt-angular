using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PFA.Migrations
{
    public partial class CreateMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpediteurId = table.Column<int>(nullable: false),
                    DestinataireId = table.Column<int>(nullable: false),
                    ExpediteurType = table.Column<string>(nullable: false, defaultValue: "User"),
                    Contenu = table.Column<string>(nullable: false),
                    DateEnvoi = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    EstLu = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ExpediteurId", // ✅ Correction de l'index pour correspondre à `ExpediteurId`
                table: "Messages",
                column: "ExpediteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DestinataireId",
                table: "Messages",
                column: "DestinataireId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
