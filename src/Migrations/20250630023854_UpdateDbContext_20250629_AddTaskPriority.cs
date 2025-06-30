using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContext_20250629_AddTaskPriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                schema: "habit_tracker",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "habit_tracker",
                table: "Tasks");
        }
    }
}
