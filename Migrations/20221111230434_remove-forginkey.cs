using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3sApp.Migrations
{
    public partial class removeforginkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_News_NewsId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Project_PostId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_PostId",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "NewsId",
                table: "Media",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Media_NewsId",
                table: "Media",
                newName: "IX_Media_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Project_ProjectId",
                table: "Media",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Project_ProjectId",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Media",
                newName: "NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_Media_ProjectId",
                table: "Media",
                newName: "IX_Media_NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_PostId",
                table: "Media",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_News_NewsId",
                table: "Media",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Project_PostId",
                table: "Media",
                column: "PostId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
