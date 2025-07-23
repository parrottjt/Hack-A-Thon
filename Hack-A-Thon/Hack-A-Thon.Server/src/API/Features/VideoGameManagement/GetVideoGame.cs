using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    public record GetVideoGames() : IRequest<List<VideoGame>>;
    public record GetVideoGame() : IRequest<List<VideoGame>>
    {
        public string? Title { get; init; } = string.Empty;
        public string? Description { get; init; } = string.Empty;
        public string? Developer { get; init; } = string.Empty;
        public string? Publisher { get; init; } = string.Empty;
        public string? Genre { get; init; } = string.Empty;
        public ESRBRating? EsrbRating { get; init; } = ESRBRating.Undefined;
    };

    public class GetVideoGamesHandler(Context context)
        : IRequestHandler<GetVideoGames, List<VideoGame>>
    {
        public Task<List<VideoGame>> Handle(GetVideoGames request, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.VideoGames.ToList());
        }
    }

    public class GetVideoGameHandler(Context context)
        : IRequestHandler<GetVideoGame, List<VideoGame>>
    {
       public Task<List<VideoGame>> Handle(GetVideoGame request, CancellationToken cancellationToken)
            {
                var search = request;
                var query = context.VideoGames.AsQueryable();

                if (string.IsNullOrEmpty(search.Title) == false)
                    query = query
                        .Where(x => x.Title.Contains(search.Title));
                if (string.IsNullOrEmpty(search.Developer) == false)
                    query = query.Where(x => x.Developer.Contains(search.Developer));
                if (string.IsNullOrEmpty(search.Publisher) == false)
                    query = query.Where(x => x.Publisher.Contains(search.Publisher));
                if (string.IsNullOrEmpty(search.Genre) == false)
                    query = query.Where(x => x.Genre.Contains(search.Genre));
                if (search.EsrbRating.HasValue)
                    query = query.Where(x => x.EsrbRating == search.EsrbRating);

                return Task.FromResult(query.ToList());
            }
   
    }
}
