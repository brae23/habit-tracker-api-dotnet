namespace HabitTracker.Api.Requests;

public class CreateTaskRequest
{
    public required string Name { get; set; }
    public required Guid ParentListId { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public TaskPriority Priority { get; set; } = TaskPriority.Low;
    public string? Notes { get; set; } = null;
}