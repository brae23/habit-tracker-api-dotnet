using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContext_20250613 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedByUserId",
                schema: "habit_tracker",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                schema: "habit_tracker",
                table: "Tasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CompletedByUserId",
                schema: "habit_tracker",
                table: "Tasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                schema: "habit_tracker",
                table: "Lists",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CompletedByUserId",
                schema: "habit_tracker",
                table: "Tasks",
                column: "CompletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_CreatedByUserId",
                schema: "habit_tracker",
                table: "Lists",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_AspNetUsers_CreatedByUserId",
                schema: "habit_tracker",
                table: "Lists",
                column: "CreatedByUserId",
                principalSchema: "habit_tracker",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_CompletedByUserId",
                schema: "habit_tracker",
                table: "Tasks",
                column: "CompletedByUserId",
                principalSchema: "habit_tracker",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedByUserId",
                schema: "habit_tracker",
                table: "Tasks",
                column: "CreatedByUserId",
                principalSchema: "habit_tracker",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_AspNetUsers_CreatedByUserId",
                schema: "habit_tracker",
                table: "Lists");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CompletedByUserId",
                schema: "habit_tracker",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedByUserId",
                schema: "habit_tracker",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CompletedByUserId",
                schema: "habit_tracker",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Lists_CreatedByUserId",
                schema: "habit_tracker",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                schema: "habit_tracker",
                table: "Lists");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                schema: "habit_tracker",
                table: "Tasks",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CompletedByUserId",
                schema: "habit_tracker",
                table: "Tasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedByUserId",
                schema: "habit_tracker",
                table: "Tasks",
                column: "CreatedByUserId",
                principalSchema: "habit_tracker",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
