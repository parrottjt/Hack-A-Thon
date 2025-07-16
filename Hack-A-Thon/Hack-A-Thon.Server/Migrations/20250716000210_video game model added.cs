using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hack_A_Thon.Server.Migrations
{
    /// <inheritdoc />
    public partial class videogamemodeladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameCoverImageSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsrbRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGames", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VideoGames",
                columns: new[] { "Id", "Description", "Developer", "EsrbRating", "GameCoverImageSrc", "Genre", "Publisher", "Title" },
                values: new object[,]
                {
                    { 1, "An open-world action RPG from the creators of Dark Souls and George R.R. Martin.", "FromSoftware", 3, "", "Action RPG", "Bandai Namco", "Elden Ring" },
                    { 2, "A sequel to Breath of the Wild that expands on its vast open-world and puzzle-solving gameplay.", "Nintendo", 1, "", "Action-Adventure", "Nintendo", "The Legend of Zelda: Tears of the Kingdom" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoGames");
        }
    }
}
