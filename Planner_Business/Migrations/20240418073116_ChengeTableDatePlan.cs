using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Business.Migrations
{
    /// <inheritdoc />
    public partial class ChengeTableDatePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "DatePlan",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DatePlan_UserId",
                table: "DatePlan",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DatePlan_User_UserId",
                table: "DatePlan",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatePlan_User_UserId",
                table: "DatePlan");

            migrationBuilder.DropIndex(
                name: "IX_DatePlan_UserId",
                table: "DatePlan");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DatePlan");
        }
    }
}
