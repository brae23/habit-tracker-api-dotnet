using HabitTracker.Api.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Api.Controllers;

[Route("api/auth")]
public class AuthController
{
    public AuthController() {}

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return new OkResult();
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        return new OkResult();
    }

    [HttpPost]
    [Route("createUser")]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        return new OkResult();
    }
}