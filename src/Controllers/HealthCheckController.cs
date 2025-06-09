using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Api.Controllers;

public class HealthCheckController : ControllerBase
{
    [Route("/healthCheck")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HealthCheck()
    {
        return StatusCode(StatusCodes.Status200OK);
    }
}