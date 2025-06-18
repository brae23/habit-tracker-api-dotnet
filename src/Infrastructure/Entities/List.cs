using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HabitTracker.Api.Models.Enums;

namespace HabitTracker.Api.Infrastructure.Entities;

public class List
{
    [Key]
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required User CreatedByUser { get; set; }
    public string? Description { get; set; }
    [ForeignKey("ParentListId")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    [ForeignKey("ParentListId")]
    public virtual ICollection<List> Sublists { get; set; } = new List<List>();
    public Guid? ParentListId { get; set; }
    public ListType Type { get; set; } = ListType.Todo;
}