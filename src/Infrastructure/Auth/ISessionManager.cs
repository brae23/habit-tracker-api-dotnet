using HabitTracker.Api.Models;

namespace HabitTracker.Api.Infrastructure.Auth;

public interface ISessionManager
{
    public void CreateSession(User user);
    public Session? GetSession(Guid sessionId);
    public void CloseSession(Guid sessionId);
}