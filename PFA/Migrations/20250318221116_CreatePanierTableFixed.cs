using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA.Migrations
{
    /// <inheritdoc />
    public partial class CreatePanierTableFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Panier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    DestinationId = table.Column<int>(nullable: true),
                    HebergementId = table.Column<int>(nullable: true),
                    ActiviteId = table.Column<int>(nullable: true),
                    TransportId = table.Column<int>(nullable: true),
                    RestaurantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panier", x => x.Id);

                    table.ForeignKey(
                        name: "FK_Panier_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",  // Assure-toi que la table s'appelle bien "Users"
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_Panier_Destination_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);

                    table.ForeignKey(
                        name: "FK_Panier_Hebergement_HebergementId",
                        column: x => x.HebergementId,
                        principalTable: "Hebergements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);

                    table.ForeignKey(
                        name: "FK_Panier_Activite_ActiviteId",
                        column: x => x.ActiviteId,
                        principalTable: "Activites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);

                    table.ForeignKey(
                        name: "FK_Panier_Transport_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);

                    table.ForeignKey(
                        name: "FK_Panier_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Panier_UserId",
                table: "Panier",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_DestinationId",
                table: "Panier",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_HebergementId",
                table: "Panier",
                column: "HebergementId");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_ActiviteId",
                table: "Panier",
                column: "ActiviteId");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_TransportId",
                table: "Panier",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_RestaurantId",
                table: "Panier",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Panier");
        }
    }
}
