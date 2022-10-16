using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieCollection.Migrations
{
    public partial class DellViewMetodFromAppMetod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewMetod",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewMetod",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
