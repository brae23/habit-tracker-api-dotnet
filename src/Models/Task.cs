namespace HabitTracker.Api.Models;

public class Task
{
    public Guid TaskId { get; set; }
    public required string Name { get; set; }
    public bool Completed { get; set; }
    public Guid CreatedByUserId { get; set; }
    public bool HasChildTasks { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid ParentListId { get; set; }
    public Guid? CompletedByUserId { get; set; }
    public string? Notes { get; set; }
    public DateTime? DueDate { get; set; }
}