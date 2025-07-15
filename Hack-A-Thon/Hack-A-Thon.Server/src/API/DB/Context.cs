using Hack_A_Thon.Server.src.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hack_A_Thon.Server.src.API.DB
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>().HasData(
                
                );
        }

        DbSet<VideoGame> VideoGames { get; set; }

    }
}
