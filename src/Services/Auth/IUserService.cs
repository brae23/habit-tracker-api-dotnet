using HabitTracker.Api.Requests.Auth;
using HabitTracker.Api.Results;

namespace HabitTracker.Api.Services.Auth;

public interface IUserService
{
    public Task<UserCRUDResult> LoginUserAsync(LoginRequest request);
    public Task<UserCRUDResult> CreateUserAsync(CreateUserRequest request);
}