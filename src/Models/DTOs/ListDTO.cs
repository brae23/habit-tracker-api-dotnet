using HabitTracker.Api.Infrastructure.Entities;

namespace HabitTracker.Api.Models.DTOs;

public class ListDTO
{
    public required Guid ListId { get; set; }
    public required string Name { get; set; }
    public required DateTime CreatedDate { get; set; }
    public string? Description { get; set; }
    public required Guid CreatedByUserId { get; set; }
    public IEnumerable<TaskDTO> ListItems { get; set; } = null!;

    public static ListDTO ToListDTO(List list)
    {
        return new ListDTO
        {
            ListId = list.Id,
            Name = list.Name,
            CreatedDate = list.CreatedDate,
            Description = list.Description,
            CreatedByUserId = Guid.TryParse(list.CreatedByUser.Id, out var createdByUserId) ? createdByUserId : Guid.Empty,
            ListItems = list.ListItems?.Select(TaskDTO.ToTaskDTO).ToList() ?? new List<TaskDTO>()
        };
    }
}