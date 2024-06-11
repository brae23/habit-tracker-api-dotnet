using System.Net;
using HabitTracker.Api.Requests.Auth;
using HabitTracker.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Api.Controllers;

[Route("api/auth")]
public class AuthController :  ControllerBase
{
    private const string SessionKey = "_UserId";
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
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
                return new NotFoundResult();
            case HttpStatusCode.BadRequest:
                return new BadRequestResult();
            case HttpStatusCode.SeeOther:
                return StatusCode(StatusCodes.Status303SeeOther);
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
            return new OkResult();
        }
        catch (Exception ex)
        {
            logger.LogError("Encountered exception while clearing user session.");
            logger.LogError($"{ex}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [Route("createUser")]
    public async Task<IActionResult> CreateUserAsync(CreateUserRequest request)
    {
        var result = await _userService.CreateUserAsync(request);

        switch (result.StatusCode)
        {
            case HttpStatusCode.Created:
                return new ObjectResult(result.User) { StatusCode = StatusCodes.Status201Created };
            case HttpStatusCode.BadRequest:
                return new BadRequestResult();
            case HttpStatusCode.Conflict:
                return new ConflictResult();
            default:
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}