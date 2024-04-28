using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Business.Migrations
{
    /// <inheritdoc />
    public partial class editRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_DatePlan_DateId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_ToDo_ToDoId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_DatePlan_User_UserId",
                table: "DatePlan");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDo_Category_CategoryId",
                table: "ToDo");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "ToDo",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "DatePlan",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToDoId",
                table: "Activity",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DateId",
                table: "Activity",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_DatePlan_DateId",
                table: "Activity",
                column: "DateId",
                principalTable: "DatePlan",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_ToDo_ToDoId",
                table: "Activity",
                column: "ToDoId",
                principalTable: "ToDo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DatePlan_User_UserId",
                table: "DatePlan",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDo_Category_CategoryId",
                table: "ToDo",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_DatePlan_DateId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_ToDo_ToDoId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_DatePlan_User_UserId",
                table: "DatePlan");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDo_Category_CategoryId",
                table: "ToDo");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "ToDo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "DatePlan",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ToDoId",
                table: "Activity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DateId",
                table: "Activity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_DatePlan_DateId",
                table: "Activity",
                column: "DateId",
                principalTable: "DatePlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_ToDo_ToDoId",
                table: "Activity",
                column: "ToDoId",
                principalTable: "ToDo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DatePlan_User_UserId",
                table: "DatePlan",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDo_Category_CategoryId",
                table: "ToDo",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
