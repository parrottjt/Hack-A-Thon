using AutoFixture;
using AutoMapper;
using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Features.VideoGameManagement;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Hack_A_Thon.Server.tests
{
    public class CreateVideoGameTests
    {
        readonly Fixture _fixture = new Fixture();
        private Context InMemoryContext()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "VideoGameDb_" + System.Guid.NewGuid())
                .Options;

            var context = new Context(options);

            return context;
        }

        [Theory]
        [InlineData("", "", "", "", "", ESRBRating.Undefined)]
        [InlineData("Elden Ring", "This is a test", "Giga", "Chad", "Pain", ESRBRating.Undefined)]
        [InlineData("Expedition 33", "A surreal turn-based RPG journey.", "Sandfall Interactive", "Kepler Interactive", "Turn-Based RPG", ESRBRating.M)]
        [InlineData("Hollow Knight", "Descend into a dark, mysterious world.", "Team Cherry", "Team Cherry", "Metroidvania", ESRBRating.T)]
        [InlineData("The Witcher 3", "A story-driven open-world RPG.", "CD Projekt Red", "CD Projekt", "RPG", ESRBRating.M)]
        [InlineData("Stardew Valley", "Build the farm of your dreams.", "ConcernedApe", "ConcernedApe", "Simulation", ESRBRating.E10)]
        public async Task CreateVideoGame_WithValidDto_ShouldReturnTrue(
            string title,
            string description,
            string developer,
            string pubisher,
            string genre,
            ESRBRating eSRBRating)
        {
            // Arrange
            using var context = InMemoryContext();
            var handler = new CreateVideoGameHandler(context);

            var dto = _fixture.Build<VideoGameDto>()
                .With(c => c.Title, title)
                .With(c => c.Description, description)
                .With(c => c.Developer, developer)
                .With(c => c.Publisher, pubisher)
                .With(c => c.Genre, genre)
                .With(c => c.EsrbRating, eSRBRating)
                .Create();

            // Act
            var result = await handler.Handle(new CreateVideoGame(dto), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(context.VideoGames);
        }
    }
}
