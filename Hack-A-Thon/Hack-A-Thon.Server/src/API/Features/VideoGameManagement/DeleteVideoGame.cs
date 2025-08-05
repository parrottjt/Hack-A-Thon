using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    public class DeleteVideoGame
    {

        public record Command : IRequest<VideoGame>
        {
            public int Id { get; init; }
        }

        public class Handler(Context context) : IRequestHandler<Command, VideoGame>
        {
            public async Task<VideoGame> Handle(Command request, CancellationToken cancellationToken)
            {
                var videoGame = context.VideoGames
                    .SingleOrDefault(x => x.Id == request.Id);

                context.Remove(videoGame);
                await context.SaveChangesAsync();

                return videoGame;
            }
        }
    }
}
