using Hack_A_Thon.Server.src.API.Infastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Hack_A_Thon.Server.src.API.DB
{
    public class Context(DbContextOptions<Context> dbContextOptions) : DbContext(dbContextOptions)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>().HasData(
                new VideoGame
                {
                    Id = 1,
                    Title= "Elden Ring",
                    Description = "An open-world action RPG from the creators of Dark Souls and George R.R. Martin.",
                    Developer = "FromSoftware",
                    Publisher = "Bandai Namco",
                    Genre = "Action RPG",
                    EsrbRating = ESRBRating.M
                },
                new VideoGame
                {
                    Id = 2,
                    Title = "The Legend of Zelda: Tears of the Kingdom",
                    Description = "A sequel to Breath of the Wild that expands on its vast open-world and puzzle-solving gameplay.",
                    Developer = "Nintendo",
                    Publisher = "Nintendo",
                    Genre = "Action-Adventure",
                    EsrbRating = ESRBRating.E10
                }
                );
        }

        public DbSet<VideoGame> VideoGames { get; set; }

    }
}
