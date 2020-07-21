using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApplication.DAL.Migrations
{
    public partial class migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Issues");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Issues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Issues");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Issues",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
