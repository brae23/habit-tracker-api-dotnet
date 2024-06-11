using HabitTracker.Api.Infrastructure.Entities;

namespace HabitTracker.Api.Models;

public class UserDTO
{
    public UserDTO(User userEntity)
    {
        UserId = userEntity.UserId;
        UserName = userEntity.UserName;
        Email = userEntity.Email;
        AccountCreatedDate = userEntity.AccountCreatedDate;
    }

    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime AccountCreatedDate { get; set; }
}