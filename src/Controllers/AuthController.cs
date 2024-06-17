using System.Net;
using FluentValidation;
using HabitTracker.Api.Requests.Auth;
using HabitTracker.Api.Services.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Api.Controllers;

[Route("api/auth")]
public class AuthController : ControllerBase
{
    private const string SessionKey = "_UserId";
    private const string SessionCookieName = ".AspNetCore.Session";
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, [FromServices] IValidator<LoginRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult);
        }

        var result = await _userService.LoginUserAsync(request);

        switch(result.StatusCode)
        {
            case HttpStatusCode.OK:
                if (result.User != null)
                {
                    HttpContext.Session.SetString(SessionKey, result.User.UserId.ToString());
                    return new OkObjectResult(result.User);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            case HttpStatusCode.NotFound:
                return StatusCode(StatusCodes.Status404NotFound, $"User with username <{request.Username}> not found.");
            case HttpStatusCode.BadRequest:
                return StatusCode(StatusCodes.Status400BadRequest, "Passwod does not match.");
            case HttpStatusCode.SeeOther:
                return StatusCode(StatusCodes.Status303SeeOther, "Password matches, but must be reset.");
            default:
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [Route("logout")]
    public IActionResult Logout([FromServices] ILogger<AuthController> logger)
    {
        try
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete(SessionCookieName);
            return new OkResult();
        }
        catch (Exception ex)
        {
            logger.LogError($"Encountered exception while clearing user session. Exception: {ex}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [Route("createUser")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request, [FromServices] IValidator<CreateUserRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult);
        }

        var result = await _userService.CreateUserAsync(request);

        switch (result.StatusCode)
        {
            case HttpStatusCode.Created:
                return StatusCode(StatusCodes.Status201Created, result.User);
            case HttpStatusCode.Conflict:
                return StatusCode(StatusCodes.Status409Conflict, $"User with username <{request.Username}> already exists.");
            default:
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}