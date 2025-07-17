using AutoFixture;
using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Features.VideoGameManagement;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Hack_A_Thon.Server.tests
{
    public class GetVideoGameTests
    {
        readonly Fixture _fixture = new Fixture();

        private Context GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "VideoGameDb_" + System.Guid.NewGuid())
                .Options;

            var context = new Context(options);

            context.VideoGames.AddRange(
                new VideoGame
                {
                    Title = "Halo Infinite",
                    Developer = "343 Industries",
                    Publisher = "Xbox Game Studios",
                    Genre = "Shooter",
                    EsrbRating = ESRBRating.T
                },
                new VideoGame
                {
                    Title = "Stardew Valley",
                    Developer = "ConcernedApe",
                    Publisher = "ConcernedApe",
                    Genre = "Simulation",
                    EsrbRating = ESRBRating.E10
                },
                new VideoGame()
                {
                    Title = "Super Mario Odyssey",
                    Developer = "Nintendo EPD",
                    Publisher = "Nintendo",
                    Genre = "Platformer",
                    EsrbRating = ESRBRating.E10
                },
                new VideoGame()
                {
                    Title = "Animal Crossing: New Horizons",
                    Developer = "Nintendo EPD",
                    Publisher = "Nintendo",
                    Genre = "Simulation",
                    EsrbRating = ESRBRating.E                }

            );

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetVideoGames_ReturnAll_ShouldReturnTrue()
        {
            // Arrange
            var context = GetInMemoryContext();
            var handler = new GetVideoGamesHandler(context);

            // Act
            var result = await handler.Handle(new GetVideoGames(), CancellationToken.None);

            // Assert
            Assert.Equal(4, result.Count);
        }

        [Theory]
        [InlineData("Halo", "", "", "", null, 1)]
        [InlineData("", "Nintendo", "Nintendo", "", null, 2)]
        [InlineData("", "", "", "", ESRBRating.E10, 2)]
        [InlineData("Halo", "", "", "", ESRBRating.E, 0)]
        [InlineData("", "","","Simulation", null, 2)]
        public async Task GetVideoGame_WithValidFilter_ShouldReturnTrue(string title, string developer, string publisher,
            string genre, ESRBRating? esrbRating, int expectedValue)
        {
            // Arrange
            var context = GetInMemoryContext();
            var handler = new GetVideoGameHandler(context);

            var command = _fixture.Build<VideoGameDto>()
                .With(c => c.Title, title)
                .With(c => c.Developer, developer)
                .With(c => c.Publisher, publisher)
                .With(c => c.EsrbRating, esrbRating)
                .With(c => c.Genre, genre)
                .Create();

            // Act
            var result = await handler.Handle(new GetVideoGame(command), CancellationToken.None);

            // Assert
            Assert.Equal(expectedValue, result.Count);
        }

        [Theory]
        [InlineData("123", "", "", "",null, 1)]
        [InlineData("", "Nintendo EPD", "N1ntendo", "",null, 2)]
        [InlineData("", "", "", "",(ESRBRating)5, 1)]
        public async Task GetVideoGame_WithInValidFilter_ShouldReturnFalse(string title, string developer, string publisher,
            string genre, ESRBRating? esrbRating, int expectedValue)
        {
            // Arrange
            var context = GetInMemoryContext();
            var handler = new GetVideoGameHandler(context);

            var command = _fixture.Build<VideoGameDto>()
                .With(c => c.Title, title)
                .With(c => c.Developer, developer)
                .With(c => c.Publisher, publisher)
                .With(c => c.EsrbRating, esrbRating)
                .With(c => c.Genre, genre)
                .Create();

            // Act
            var result = await handler.Handle(new GetVideoGame(command), CancellationToken.None);

            // Assert
            Assert.NotEqual(expectedValue, result.Count);
        }
    }
}
