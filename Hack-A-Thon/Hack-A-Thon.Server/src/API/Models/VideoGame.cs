namespace Hack_A_Thon.Server.src.API.Models
{
    public class VideoGame
    {
        public int Id { get; set; }
        public string? GameCoverImageSrc { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Developer { get; set; } = "";
        public string Publisher { get; set; } = "";
        public string Genre { get; set; } = "";
        public int EsrbRating { get; set; }
        public float CriticScore { get; set; }
        public float UserScore { get; set; }
    }
}
