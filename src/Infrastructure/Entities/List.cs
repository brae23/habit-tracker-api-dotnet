using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitTracker.Api.Infrastructure.Entities;

public class List
{
    [Key]
    public required Guid Id { get; set; }
    public required string Name { get; set;}
    public required DateTime CreatedDate { get; set; }
    public required User CreatedByUser { get; set; }
    public string? Description { get; set; }
    [ForeignKey("ParentListId")]
    public IEnumerable<Task>? ListItems { get; set; }
}