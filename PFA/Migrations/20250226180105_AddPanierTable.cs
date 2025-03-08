using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA.Migrations
{
    /// <inheritdoc />
    public partial class AddPanierTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paniers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: true),
                    HebergementId = table.Column<int>(type: "int", nullable: true),
                    ActiviteId = table.Column<int>(type: "int", nullable: true),
                    TransportId = table.Column<int>(type: "int", nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paniers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paniers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paniers_Activites_ActiviteId",
                        column: x => x.ActiviteId,
                        principalTable: "Activites",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paniers_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paniers_Hebergements_HebergementId",
                        column: x => x.HebergementId,
                        principalTable: "Hebergements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paniers_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paniers_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paniers_UserId",
                table: "Paniers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Paniers_ActiviteId",
                table: "Paniers",
                column: "ActiviteId");

            migrationBuilder.CreateIndex(
                name: "IX_Paniers_DestinationId",
                table: "Paniers",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Paniers_HebergementId",
                table: "Paniers",
                column: "HebergementId");

            migrationBuilder.CreateIndex(
                name: "IX_Paniers_RestaurantId",
                table: "Paniers",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Paniers_TransportId",
                table: "Paniers",
                column: "TransportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paniers");
        }
    }

}
