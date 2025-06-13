using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public Guid ParentListId { get; set; }
    public User? CompletedByUser { get; set; }
    public string? Notes { get; set; }
    public DateTime? DueDate { get; set; }
}