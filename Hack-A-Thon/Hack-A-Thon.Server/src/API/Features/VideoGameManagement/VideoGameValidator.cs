using FluentValidation;
using Hack_A_Thon.Server.src.API.Infastructure.Models;

namespace Hack_A_Thon.Server.src.API.Features.VideoGameManagement
{
    public class VideoGameValidator : AbstractValidator<VideoGame>
    {
        public VideoGameValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
