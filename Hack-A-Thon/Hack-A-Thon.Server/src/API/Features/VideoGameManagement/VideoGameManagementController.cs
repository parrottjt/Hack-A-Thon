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
        public async Task<List<VideoGame>> Get([FromQuery] GetVideoGame command)
        {
            return await mediator.Send(command);
        }

        [HttpPost("Create")]
        public async Task<VideoGame> Create([FromBody] CreateVideoGame command)
        {
            return await mediator.Send(command);
        }

        [HttpDelete("Delete")]
        public async Task<VideoGame> Delete([FromBody] DeleteVideoGame command)
        {
            return await mediator.Send(command);
        }

        [HttpPut("Update")]
        public async Task<VideoGame> Put([FromBody] UpdateVideoGame.Command command)
        {
            return await mediator.Send(command);
        }
    }
}
