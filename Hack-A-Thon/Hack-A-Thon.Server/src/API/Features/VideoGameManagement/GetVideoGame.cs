using System.Diagnostics;
using AutoMapper;
using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    public record GetVideoGame(VideoGameDto videoGame) : IRequest<List<VideoGame>>;
    public record GetVideoGames() : IRequest<List<VideoGame>>;

    public class GetVideoGameHandler(Context context, IMediator mediator, IMapper mapper)
        : IRequestHandler<GetVideoGame, List<VideoGame>>
    {
        private readonly Context _context = context;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        //public Task<List<VideoGame>> Handle(GetVideoGame request, CancellationToken cancellationToken)
        //{
        //    var search = request.videoGame;
        //    var query = context.VideoGames.AsQueryable();

        //    if (string.IsNullOrEmpty(search.Title) == false) 
        //        query = query.Where(x => x.Title.Contains(search.Title));
        //    if (string.IsNullOrEmpty(search.Developer) == false)
        //        query = query.Where(x => x.Developer.Contains(search.Developer));
        //    if (string.IsNullOrEmpty(search.Publisher) == false)
        //        query = query.Where(x => x.Publisher.Contains(search.Publisher));
        //    if (string.IsNullOrEmpty(search.Genre) == false)
        //        query = query.Where(x => x.Genre.Contains(search.Genre));
        //    if (search.EsrbRating.HasValue)
        //        query = query.Where(x => x.EsrbRating == search.EsrbRating);
            
        //    return Task.FromResult(query.ToList());
        //}
        public Task<List<VideoGame>> Handle(GetVideoGame request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Hit");
            return Task.FromResult(_context.VideoGames.ToList());
        }
    }
}
