using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitTracker.Api.Infrastructure.Entities;

public class List
{
    [Key]
    public Guid ListId { get; set; }
    public required string Name { get; set;}
    public DateTime CreatedDate { get; set; }
    public string? Description { get; set; }
    [ForeignKey("ParentListId")]
    public IEnumerable<Task>? ListItems { get; set; }
}