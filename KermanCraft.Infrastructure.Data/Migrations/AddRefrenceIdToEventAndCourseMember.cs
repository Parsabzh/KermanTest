using Microsoft.EntityFrameworkCore.Migrations;

namespace KermanCraft.Infrastructure.Data.Migrations
{
    public partial class AddRefrenceIdToEventAndCourseMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceId",
                table: "EventMembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceId",
                table: "CourseMembers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "EventMembers");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "CourseMembers");
        }
    }
}
