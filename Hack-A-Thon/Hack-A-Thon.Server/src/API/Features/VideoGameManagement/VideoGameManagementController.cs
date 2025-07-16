using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    
    [ApiController]
    public class VideoGameManagementController(IMediator mediator, Context context) : ControllerBase
    {
        private readonly Context _context = context;

        [HttpGet("VideoGames")]
        public async Task<ActionResult<List<VideoGame>>> Get()
        {
            return await mediator.Send(new GetVideoGames());
        }
        //[HttpGet("VideoGame")]
        //public async Task<List<VideoGame>> Get(VideoGameDto dto)
        //{
        //    return await mediator.Send(new GetVideoGame(dto));
        //}
    }
}
