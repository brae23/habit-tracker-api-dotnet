namespace HabitTracker.Api.Models.DTOs;

public class TaskDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool Completed { get; set; }
    public Guid CreatedByUserId { get; set; }
    public bool HasChildTasks { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? ParentListId { get; set; } = null;
    public Guid? CompletedByUserId { get; set; }
    public string? Notes { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Low;

    public static TaskDTO ToTaskDTO(Infrastructure.Entities.Task task)
    {
        return new TaskDTO
        {
            Id = task.Id,
            Name = task.Name,
            Completed = task.Completed,
            CreatedByUserId = Guid.TryParse(task.CreatedByUser.Id, out var createdByUserId) ? createdByUserId : Guid.Empty,
            CreatedDate = task.CreatedDate,
            HasChildTasks = task.HasChildTasks,
            ParentListId = task.ParentListId,
            CompletedByUserId = Guid.TryParse(task.CompletedByUser?.Id, out var completedByUserId) ? completedByUserId : null,
            Notes = task.Notes,
            DueDate = task.DueDate,
            Priority = task.Priority
        };
    }
}