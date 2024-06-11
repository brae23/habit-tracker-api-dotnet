using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Api.Infrastructure.Entities;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public DateTime AccountCreatedDate { get; set; }
    public required string PasswordHash { get; set; }
}