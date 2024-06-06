using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HabitTrackerApi.Models;

namespace HabitTrackerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTestData")]
    public TestData Get()
    {
        return new TestData { Message = "Hello World!" };
    }
}
