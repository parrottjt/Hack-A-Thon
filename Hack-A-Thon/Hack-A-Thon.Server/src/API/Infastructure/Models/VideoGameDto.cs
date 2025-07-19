using Microsoft.AspNetCore.Mvc;

namespace Hack_A_Thon.Server.src.API.Infastructure.Models
{
    public class VideoGameDto()
    {
        public string? GameCoverImageSrc { get; set; } = "";
        public string? Title { get; set; } = "";
        public string? Description { get; set; } = "";
        public string? Developer { get; set; } = "";
        public string? Publisher { get; set; } = "";
        public string? Genre { get; set; } = "";

        [FromQuery]
        public ESRBRating? EsrbRating { get; set; } = ESRBRating.Undefined;
    }
}
