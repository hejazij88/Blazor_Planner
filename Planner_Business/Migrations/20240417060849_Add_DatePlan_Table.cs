using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Business.Migrations
{
    /// <inheritdoc />
    public partial class Add_DatePlan_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Activity");

            migrationBuilder.AddColumn<Guid>(
                name: "DateId",
                table: "Activity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DatePlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatePlan", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_DateId",
                table: "Activity",
                column: "DateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_DatePlan_DateId",
                table: "Activity",
                column: "DateId",
                principalTable: "DatePlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_DatePlan_DateId",
                table: "Activity");

            migrationBuilder.DropTable(
                name: "DatePlan");

            migrationBuilder.DropIndex(
                name: "IX_Activity_DateId",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "DateId",
                table: "Activity");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Activity",
                type: "datetime2",
                nullable: true);
        }
    }
}
