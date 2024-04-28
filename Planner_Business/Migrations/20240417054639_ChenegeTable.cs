using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Business.Migrations
{
    /// <inheritdoc />
    public partial class ChenegeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoTime",
                table: "Activity",
                newName: "DateTime");

            migrationBuilder.AddColumn<bool>(
                name: "IsDo",
                table: "Activity",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDo",
                table: "Activity");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Activity",
                newName: "DoTime");
        }
    }
}
