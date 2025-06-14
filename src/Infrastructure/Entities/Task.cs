using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Api.Infrastructure.Entities;

public class Task
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool Completed { get; set; }
    public required User CreatedByUser { get; set; }
    public bool HasChildTasks { get; set; }
    public DateTime CreatedDate { get; set; }
    public User? CompletedByUser { get; set; }
    public string? Notes { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid? ParentListId { get; set; }
}