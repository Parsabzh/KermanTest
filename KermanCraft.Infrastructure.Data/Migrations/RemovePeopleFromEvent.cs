using Microsoft.EntityFrameworkCore.Migrations;

namespace KermanCraft.Infrastructure.Data.Migrations
{
    public partial class RemovePeopleFromEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_PeopleId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_PeopleId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PeopleId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "PeopleFkId",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeopleFkId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "PeopleId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_PeopleId",
                table: "Events",
                column: "PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_PeopleId",
                table: "Events",
                column: "PeopleId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
