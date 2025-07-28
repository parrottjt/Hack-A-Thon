using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    public record DeleteVideoGame : IRequest<VideoGame>
    {
        public int Id { get; init; }
    }

    public class DeleteVideoGameHandler(Context context) : IRequestHandler<DeleteVideoGame, VideoGame>
    {
        public async Task<VideoGame> Handle(DeleteVideoGame request, CancellationToken cancellationToken)
        {
            var videoGame = context.VideoGames
                .SingleOrDefault(x => x.Id == request.Id);

            context.Remove(videoGame);
            await context.SaveChangesAsync();

            return await Task.FromResult(videoGame);
        }
    }
}
