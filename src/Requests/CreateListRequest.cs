using HabitTracker.Api.Infrastructure.Entities;
using HabitTracker.Api.Models.Enums;

namespace HabitTracker.Api.Requests;

public class CreateListRequest
{
    public required string Name { get; set; }
    public required ListType Type { get; set; }
    public string? Description { get; set; }
}