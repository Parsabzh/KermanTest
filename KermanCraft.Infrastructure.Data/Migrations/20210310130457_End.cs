using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KermanCraft.Infrastructure.Data.Migrations
{
    public partial class End : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Comments");
        }
    }
}
