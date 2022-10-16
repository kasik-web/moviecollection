using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieCollection.Migrations
{
    public partial class AddtrailerUrlToMovieDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrailerUrl",
                table: "MovieDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrailerUrl",
                table: "MovieDetails");
        }
    }
}
