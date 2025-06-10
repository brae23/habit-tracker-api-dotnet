namespace HabitTracker.Api.Processors;

using HabitTracker.Api.Models;
using HabitTracker.Api.Infrastructure.Entities;

public class ListProcessor
{
    public static ListDTO ToListDTO(List list)
    {
        return new ListDTO
        {
            ListId = list.Id,
            Name = list.Name,
            CreatedDate = list.CreatedDate,
            Description = list.Description,
            ListItems = list.ListItems?.Select(task => new TaskDTO
            {
                TaskId = task.Id,
                Name = task.Name,
                Completed = task.Completed,
                CreatedByUserId = Guid.TryParse(task.CreatedByUserId, out var userId) ? userId : Guid.Empty,
                CreatedDate = task.CreatedDate,
                HasChildTasks = task.HasChildTasks,
                ParentListId = task.ParentListId,
                CompletedByUserId = task.CompletedByUserId,
                Notes = task.Notes,
                DueDate = task.DueDate
            }).ToList() ?? new List<TaskDTO>()
        };
    }
}