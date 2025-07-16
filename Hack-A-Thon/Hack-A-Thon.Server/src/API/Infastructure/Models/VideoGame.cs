namespace Hack_A_Thon.Server.src.API.Infastructure.Models
{
    public enum ESRBRating
    {
        Everyone,
        Everyone10Plus,
        Teen,
        Mature,
        Adult
    }
    public class VideoGame
    {
        public int Id { get; set; } = 0;
        public string? GameCoverImageSrc { get; set; } = "";
        public string Title { get; set; } = "";
        public string? Description { get; set; } = "";
        public string Developer { get; set; } = "";
        public string Publisher { get; set; } = "";
        public string Genre { get; set; } = "";
        public ESRBRating EsrbRating { get; set; } = ESRBRating.Everyone;
    }
}
