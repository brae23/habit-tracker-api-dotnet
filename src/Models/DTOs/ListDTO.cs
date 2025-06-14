using HabitTracker.Api.Infrastructure.Entities;

namespace HabitTracker.Api.Models.DTOs;

public class ListDTO
{
    public required Guid ListId { get; set; }
    public required string Name { get; set; }
    public required DateTime CreatedDate { get; set; }
    public string? Description { get; set; }
    public required Guid CreatedByUserId { get; set; }
    public IEnumerable<TaskDTO> Tasks { get; set; } = null!;
    public IEnumerable<ListDTO> Sublists { get; set; } = null!;

    public Guid? ParentListId { get; set; } = null;

    public static ListDTO ToListDTO(List list)
    {
        return new ListDTO
        {
            ListId = list.Id,
            Name = list.Name,
            CreatedDate = list.CreatedDate,
            Description = list.Description,
            CreatedByUserId = Guid.TryParse(list.CreatedByUser.Id, out var createdByUserId) ? createdByUserId : Guid.Empty,
            Tasks = list.Tasks.Select(TaskDTO.ToTaskDTO),
            Sublists = list.Sublists.Select(ToListDTO),
            ParentListId = list.ParentListId
        };
    }
}