using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PFA.Controllers;
using PFA.Data;
using PFA.Models;

namespace PFA.Tests
{
    public class DestinationsControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var ctx = new AppDbContext(options);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            // Seed initial des destinations
            ctx.Destinations.AddRange(
                new Destination
                {
                    Id = 1,
                    Ville = "Casablanca",
                    Description = "Ville portuaire"
                },
                new Destination
                {
                    Id = 2,
                    Ville = "Marrakech",
                    Description = "Ville impériale"
                }
            );
            ctx.SaveChanges();

            return ctx;
        }

        [Fact]
        public async Task GetDestinations_ReturnsAllDestinations()
        {
            // Arrange
            var ctx = GetInMemoryDbContext();
            var ctrl = new DestinationsController(ctx);

            // Act
            var actionResult = await ctrl.GetDestinations();

            // Assert: on s'assure que Value contient bien une liste de 2 éléments
            var list = Assert.IsType<List<Destination>>(actionResult.Value);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task GetDestination_ReturnsNotFound_WhenIdIsInvalid()
        {
            // Arrange
            var ctx = GetInMemoryDbContext();
            var ctrl = new DestinationsController(ctx);

            // Act
            var actionResult = await ctrl.GetDestination(99);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("Destination non trouvée", notFound.Value);
        }

        [Fact]
        public async Task PostDestination_AddsNewDestination()
        {
            // Arrange
            var ctx = GetInMemoryDbContext();
            var ctrl = new DestinationsController(ctx);

            var newDest = new Destination
            {
                Id = 3,
                Ville = "Agadir",
                Description = "Ville balnéaire"
            };

            // Act
            var actionResult = await ctrl.PostDestination(newDest);

            // Assert: on récupère CreatedAtActionResult
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var createdDest = Assert.IsType<Destination>(createdResult.Value);
            Assert.Equal("Agadir", createdDest.Ville);
            Assert.Equal("Ville balnéaire", createdDest.Description);
        }
    }
}