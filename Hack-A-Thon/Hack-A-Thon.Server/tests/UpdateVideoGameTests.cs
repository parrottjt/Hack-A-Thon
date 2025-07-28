using AutoFixture;
using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Features.VideoGameManagement;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Hack_A_Thon.Server.tests
{
    public class UpdateVideoGameTests
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
                }
            );

            context.SaveChanges();
            return context;
        }

        [Theory]
        [InlineData("Halo","Fun Game", "343 Industries", "Xbox Game Studios", "Shooter", "Undefined", ESRBRating.Undefined)]
        public async Task UpdateVideoGame_WithValidUpdate_ShouldReturnTrue( 
            string title,
            string description,
            string developer,
            string pubisher,
            string genre,
            string eSRBRating,
            ESRBRating expectedRating)
        {
            using var context = GetInMemoryContext();
            var handler = new UpdateVideoGame.Handler(context);

            var command = _fixture.Build<UpdateVideoGame.Command>()
                .With(c => c.Id, 1)
                .With(c => c.Title, title)
                .With(c => c.Description, description)
                .With(c => c.Developer, developer)
                .With(c => c.Publisher, pubisher)
                .With(c => c.Genre, genre)
                .With(c => c.EsrbRating, eSRBRating)
                .Create();

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(title, result.Title);
            Assert.Equal(description, result.Description);
            Assert.Equal(developer, result.Developer);
            Assert.Equal(expectedRating, result.EsrbRating);
        }
    }
}
