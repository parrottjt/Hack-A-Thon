namespace Hack_A_Thon.Server.src.API.Infastructure.Models
{
    public class VideoGameDto()
    {
        public string? Title { get; set; } = "";
        public string? Developer { get; set; } = "";
        public string? Publisher { get; set; } = "";
        public string? Genre { get; set; } = "";
        public ESRBRating? EsrbRating { get; set; }
    }
}
