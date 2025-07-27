using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    public class UpdateVideoGame
    {
        public class Command : IRequest<VideoGame>
        {
            public int Id { get; set; }
            public string? GameCoverImageSrc { get; init; } = string.Empty;
            public string? Title { get; init; } = string.Empty;
            public string? Description { get; init; } = string.Empty;
            public string? Developer { get; init; } = string.Empty;
            public string? Publisher { get; init; } = string.Empty;
            public string? Genre { get; init; } = string.Empty;
            public string? EsrbRating { get; set; }
        }

        public class CommandHandler(Context context) : IRequestHandler<Command, VideoGame>
        {
            public Task<VideoGame> Handle(Command request, CancellationToken cancellationToken)
            {
                var videoGame = context.VideoGames
                    .Where(x => x.Id == request.Id)
                    .Single();

                if (string.IsNullOrEmpty(request.GameCoverImageSrc) == false)
                    videoGame.GameCoverImageSrc = request.GameCoverImageSrc;
                if (string.IsNullOrEmpty(request.Title) == false)
                    videoGame.Title = request.Title;
                if (string.IsNullOrEmpty(request.Description) == false)
                    videoGame.Description = request.Description;
                if (string.IsNullOrEmpty(request.Developer) == false)
                    videoGame.Developer = request.Developer;
                if (string.IsNullOrEmpty(request.Publisher) == false)
                    videoGame.Publisher = request.Publisher;
                if (string.IsNullOrEmpty(request.Genre) == false)
                    videoGame.Genre = request.Genre;
                if (Enum.TryParse<ESRBRating>(request.EsrbRating, true, out var result))
                    videoGame.EsrbRating = result;

                context.VideoGames.Update(videoGame);
                context.SaveChangesAsync();

                return Task.FromResult(videoGame);
            }
        }
    }
}
