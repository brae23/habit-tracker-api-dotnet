using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddParentListIdForeignKeyAssocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Lists_ListId",
                schema: "habit_tracker",
                table: "Lists");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Lists_ListId",
                schema: "habit_tracker",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ListId",
                schema: "habit_tracker",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Lists_ListId",
                schema: "habit_tracker",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "ListId",
                schema: "habit_tracker",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ListId",
                schema: "habit_tracker",
                table: "Lists");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ParentListId",
                schema: "habit_tracker",
                table: "Tasks",
                column: "ParentListId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_ParentListId",
                schema: "habit_tracker",
                table: "Lists",
                column: "ParentListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Lists_ParentListId",
                schema: "habit_tracker",
                table: "Lists",
                column: "ParentListId",
                principalSchema: "habit_tracker",
                principalTable: "Lists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Lists_ParentListId",
                schema: "habit_tracker",
                table: "Tasks",
                column: "ParentListId",
                principalSchema: "habit_tracker",
                principalTable: "Lists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Lists_ParentListId",
                schema: "habit_tracker",
                table: "Lists");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Lists_ParentListId",
                schema: "habit_tracker",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ParentListId",
                schema: "habit_tracker",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Lists_ParentListId",
                schema: "habit_tracker",
                table: "Lists");

            migrationBuilder.AddColumn<Guid>(
                name: "ListId",
                schema: "habit_tracker",
                table: "Tasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ListId",
                schema: "habit_tracker",
                table: "Lists",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ListId",
                schema: "habit_tracker",
                table: "Tasks",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_ListId",
                schema: "habit_tracker",
                table: "Lists",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Lists_ListId",
                schema: "habit_tracker",
                table: "Lists",
                column: "ListId",
                principalSchema: "habit_tracker",
                principalTable: "Lists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Lists_ListId",
                schema: "habit_tracker",
                table: "Tasks",
                column: "ListId",
                principalSchema: "habit_tracker",
                principalTable: "Lists",
                principalColumn: "Id");
        }
    }
}
