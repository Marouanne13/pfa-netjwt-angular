using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using PFA.Controllers;
using PFA.Data;
using PFA.Models;

namespace PFA.Tests
{
    public class HebergementControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var ctx = new AppDbContext(options);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            // Seed minimal pour permettre le Delete
            ctx.Destinations.Add(new Destination
            {
                Id = 1,
                Ville = "Rabat",
                Description = "Capitale administrative du Maroc"
            });
            ctx.Hebergements.Add(new Hebergement
            {
                Id = 1,
                Nom = "Hotel A",
                Type = "Hôtel",
                Adresse = "Rue 1",
                PrixParNuit = 200,
                DestinationId = 1,
                ClassementEtoiles = 3,
                PetitDejeunerInclus = true,
                Piscine = false
            });
            ctx.SaveChanges();

            return ctx;
        }

        [Fact]
        public async Task SupprimerHebergement_ValidId_ReturnsNoContent()
        {
            // Arrange
            var ctx = GetInMemoryDbContext();
            var ctrl = new HebergementController(ctx);

            // Act
            var result = await ctrl.SupprimerHebergement(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
