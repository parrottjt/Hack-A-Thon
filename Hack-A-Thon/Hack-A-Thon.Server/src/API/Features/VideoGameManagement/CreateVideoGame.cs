using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    public class CreateVideoGame
    {
        public record Command : IRequest<VideoGame>
        {
            public string? GameCoverImageSrc { get; init; } = string.Empty;
            public string Title { get; init; } = string.Empty;
            public string? Description { get; init; } = string.Empty;
            public string? Developer { get; init; } = string.Empty;
            public string? Publisher { get; init; } = string.Empty;
            public string? Genre { get; init; } = string.Empty;
            public string? EsrbRating { get; init; }
        };

        public class Handler(Context context) : IRequestHandler<Command, VideoGame>
        {
            public async Task<VideoGame> Handle(Command request, CancellationToken cancellationToken)
            {
                VideoGame videoGame = new VideoGame
                {
                    Title = request.Title,
                    GameCoverImageSrc = request.GameCoverImageSrc,
                    Description = request.Description is not null ? request.Description : "",
                    Developer = request.Developer is not null ? request.Developer : "",
                    Publisher = request.Publisher is not null ? request.Publisher : "",
                    Genre = request.Genre is not null ? request.Genre : "",
                    EsrbRating = Enum.TryParse<ESRBRating>(request.EsrbRating, out var result) ? result : ESRBRating.Undefined
                };

                context.VideoGames.Add(videoGame);
                await context.SaveChangesAsync();

                return await Task.FromResult(videoGame);
            }
        }
    }
}
