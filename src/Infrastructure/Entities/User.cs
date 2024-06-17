using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitTracker.Api.Infrastructure.Entities;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime AccountCreatedDate { get; set; }
    public required string PasswordHash { get; set; }
}