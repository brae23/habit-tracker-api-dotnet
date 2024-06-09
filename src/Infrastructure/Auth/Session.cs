using HabitTracker.Api.Models;

namespace HabitTracker.Api.Infrastructure.Auth;

public class Session
{
    public Guid SessionId { get; }
    public User User { get; }
    public Session(User user)
    {
        SessionId = new Guid();
        User = user;
    }
}