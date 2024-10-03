using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace practice_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampsDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Department",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Department",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Department",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Department");
        }
    }
}
