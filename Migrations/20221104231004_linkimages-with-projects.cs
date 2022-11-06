using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3sApp.Migrations
{
    public partial class linkimageswithprojects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Media_PostId",
                table: "Media",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Project_PostId",
                table: "Media",
                column: "PostId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Project_PostId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_PostId",
                table: "Media");
        }
    }
}
