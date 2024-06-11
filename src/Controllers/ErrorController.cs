using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Api.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError()
    {
        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}