using HabitTracker.Api.Models;

namespace HabitTracker.Api.Infrastructure.Auth;

public class SessionManager : ISessionManager
{
    private readonly ILogger<SessionManager> _logger;
    private IList<Session> _activeSessions;

    public SessionManager(ILogger<SessionManager> logger)
    {
        _logger = logger;
        _activeSessions = new List<Session>();
    }

    public void CreateSession(User user)
    {
        var session = new Session(user);
        _activeSessions.Append(session);
        _logger.LogInformation($"New session for user <{user.UserId}> created. SessionId is <{session.SessionId}>");
    }

    public Session? GetSession(Guid sessionId)
    {
        return _activeSessions.FirstOrDefault(x => x.SessionId == sessionId);
    }

    public void CloseSession(Guid sessionId)
    {
        var session = GetSession(sessionId);

        if (session == null)
        {
            _logger.LogError($"Session with sessionId <{sessionId}> not found. No session to close.");
            return;
        }

        _activeSessions.Remove(session);
        _logger.LogInformation($"Session with sessionId <{sessionId}> closed");
    }
}