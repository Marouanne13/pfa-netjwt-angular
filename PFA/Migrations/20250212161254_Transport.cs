﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA.Migrations
{
    /// <inheritdoc />
    public partial class Transport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeTransport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTransport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacite = table.Column<int>(type: "int", nullable: false),
                    EstDisponible = table.Column<bool>(type: "bit", nullable: false),
                    TypeTransportId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomCompagnie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModeDeTransport = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroImmatriculation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeureDepart = table.Column<TimeSpan>(type: "time", nullable: false),
                    HeureArrivee = table.Column<TimeSpan>(type: "time", nullable: false),
                    NumeroServiceClient = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transport_TypeTransport_TypeTransportId",
                        column: x => x.TypeTransportId,
                        principalTable: "TypeTransport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transport_Users_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transport_TypeTransportId",
                table: "Transport",
                column: "TypeTransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_UtilisateurId",
                table: "Transport",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transport");

            migrationBuilder.DropTable(
                name: "TypeTransport");
        }
    }
}
