using HabitTracker.Api.Infrastructure.Entities;

namespace HabitTracker.Api.Models;

public class UserDTO
{
    public UserDTO(User userEntity)
    {
        UserId = userEntity.Id;
        UserName = userEntity.UserName ?? string.Empty;
        Email = userEntity.Email ?? string.Empty;
        FirstName = userEntity.FirstName ?? string.Empty;;
        LastName = userEntity.LastName ?? string.Empty;;
        AccountCreatedDate = userEntity.AccountCreatedDate;
    }

    public string UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime AccountCreatedDate { get; set; }
}