using Hack_A_Thon.Server.src.API.DB;
using Hack_A_Thon.Server.src.API.Infastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    
    [ApiController]
    public class VideoGameManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly Context _context;

        public VideoGameManagementController(IMediator mediator, Context context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet("VideoGames")]
        public async Task<ActionResult<List<VideoGame>>> Get()
        {
            return await _mediator.Send(new GetVideoGames());
        }
        [HttpGet("VideoGame")]
        public async Task<List<VideoGame>> Get(VideoGameDto dto)
        {
            return await _mediator.Send(new GetVideoGame(dto));
        }
    }
}
