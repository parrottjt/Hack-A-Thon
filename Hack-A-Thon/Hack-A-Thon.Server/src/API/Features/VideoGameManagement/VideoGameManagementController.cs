using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    [ApiController]
    public class VideoGameManagementController(IMediator mediator) : ControllerBase
    {
        [HttpGet("VideoGames")]
        public async Task<ActionResult<List<VideoGame>>> Get()
        {
            return await mediator.Send(new GetVideoGames());
        }

        
        [HttpGet("VideoGame")]
        public async Task<List<VideoGame>> Get([FromQuery]VideoGameDto dto)
        {
            return await mediator.Send(new GetVideoGame(dto));
        }

        [HttpPost("Create")]
        public async Task<VideoGame> Create([FromBody] VideoGameDto dto)
        {
            return await mediator.Send(new CreateVideoGame(dto));
        }
    }
}
