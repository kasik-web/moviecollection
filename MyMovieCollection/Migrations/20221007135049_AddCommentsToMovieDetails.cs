using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieCollection.Migrations
{
    public partial class AddCommentsToMovieDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "MovieDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "MovieDetails");
        }
    }
}
