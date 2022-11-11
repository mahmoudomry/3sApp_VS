using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3sApp.Migrations
{
    public partial class editprojects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "Media",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_NewsId",
                table: "Media",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_News_NewsId",
                table: "Media",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_News_NewsId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_NewsId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Media");
        }
    }
}
