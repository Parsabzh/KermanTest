using Microsoft.EntityFrameworkCore.Migrations;

namespace KermanCraft.Infrastructure.Data.Migrations
{
    public partial class AddVideoUrlToWindow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Windows",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Windows");
        }
    }
}
