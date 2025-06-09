using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Api.Infrastructure.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime AccountCreatedDate { get; set; }
}