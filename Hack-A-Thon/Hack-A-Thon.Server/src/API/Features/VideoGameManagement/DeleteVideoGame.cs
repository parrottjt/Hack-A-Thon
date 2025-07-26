using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    public record DeleteVideoGame : IRequest<VideoGame>
    {
        public int Id { get; init; }
        public string Title { get; init; }
    }

    public class DeleteVideoGameHandler(Context context) : IRequestHandler<DeleteVideoGame, VideoGame>
    {
        public async Task<VideoGame> Handle(DeleteVideoGame request, CancellationToken cancellationToken)
        {
            var query = context.VideoGames.AsQueryable();

            if (string.IsNullOrEmpty(request.Title) == false)
                query = query.Where(x => x.Title.Contains(request.Title));
            if (request.Id >= 0)
                query = query.Where(x => x.Id == request.Id);

            var videoGame = query.SingleOrDefault();

            context.Remove(videoGame);
            await context.SaveChangesAsync();

            return await Task.FromResult(videoGame);
        }
    }
}
