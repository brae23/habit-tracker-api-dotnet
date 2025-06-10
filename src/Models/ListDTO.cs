namespace HabitTracker.Api.Models;

public class ListDTO
{
    public Guid ListId { get; set; }
    public required string Name { get; set;}
    public DateTime CreatedDate { get; set; }
    public string? Description { get; set; }
    public IEnumerable<TaskDTO> ListItems { get; set; } = null!;
}