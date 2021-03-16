using Microsoft.EntityFrameworkCore.Migrations;

namespace KermanCraft.Infrastructure.Data.Migrations
{
    public partial class AddAchievementToPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Achievement",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Achievement",
                table: "AspNetUsers");
        }
    }
}
