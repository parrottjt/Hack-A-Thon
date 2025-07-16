using AutoMapper;
using Hack_A_Thon.Server.src.API.Infastructure.Models;

namespace Hack_A_Thon.Server.src.API.Infastructure
{
    public class VideoGameProfile : Profile
    {
        public VideoGameProfile()
        {
            CreateMap<VideoGame, VideoGameDto>();
        }
    }
}
