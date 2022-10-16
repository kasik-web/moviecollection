using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieCollection.Migrations
{
    public partial class DellOriginalTitle_cancel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalTitle",
                table: "MovieDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalTitle",
                table: "MovieDetails");
        }
    }
}
