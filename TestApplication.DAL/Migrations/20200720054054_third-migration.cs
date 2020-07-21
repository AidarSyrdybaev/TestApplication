using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApplication.DAL.Migrations
{
    public partial class thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Issues_ManagerId",
                table: "Issues",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_ManagerId",
                table: "Issues",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_ManagerId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ManagerId",
                table: "Issues");
        }
    }
}
