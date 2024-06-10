namespace HabitTracker.Api.Models;

public class User
{
    public Guid UserId { get; set; }
    public required string UserName { get; set; } = string.Empty;
    public DateTime AccountCreatedDate { get; set; }
}