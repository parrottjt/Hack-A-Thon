using AutoMapper;
using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    public record CreateVideoGame(VideoGameDto dto) : IRequest<VideoGame>;

    public class CreateVideoGameHandler(Context context) : IRequestHandler<CreateVideoGame, VideoGame>
    {
        public async Task<VideoGame> Handle(CreateVideoGame request, CancellationToken cancellationToken)
        {
            VideoGame videoGame = new VideoGame
            {
                Title = request.dto.Title is not null ? request.dto.Title : "",
                GameCoverImageSrc = request.dto.GameCoverImageSrc,
                Description = request.dto.Description,
                Developer = request.dto.Developer is not null ? request.dto.Developer : "",
                Publisher = request.dto.Publisher is not null ? request.dto.Publisher : "",
                Genre = request.dto.Genre is not null ? request.dto.Genre : "",
                EsrbRating = (ESRBRating)(request.dto.EsrbRating is not null ? request.dto.EsrbRating : ESRBRating.Undefined)
            };

            context.VideoGames.Add(videoGame);
            await context.SaveChangesAsync();

            return await Task.FromResult(videoGame);
        }
    }
}
