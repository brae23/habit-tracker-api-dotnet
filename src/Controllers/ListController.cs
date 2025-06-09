using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Api.Controllers;

public class ListController : ControllerBase
{
    [Route("/api/lists")]
    [Authorize]
    [HttpGet]
    public IActionResult GetLists()
    {
        return StatusCode(StatusCodes.Status200OK, new
        {
            Message = "This is a placeholder for the lists endpoint. Implement your logic here."
        });
    }
}