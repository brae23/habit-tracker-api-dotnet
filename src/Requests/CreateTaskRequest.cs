namespace HabitTracker.Api.Requests;

public class CreateTaskRequest
{
    public required string Name { get; set; }
    public required Guid ListId { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public int Priority { get; set; } = 0;
    public string? Notes { get; set; } = null;
}