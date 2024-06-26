using Microsoft.AspNetCore.Mvc.Testing;

namespace HabitTracker.Api.IntegrationTests.Infrastructure;

public abstract class IntegrationTestBase
{
    protected IntegrationTestBase()
    {
        var webApplicationFactory = new WebApplicationFactory<Program>();
        HttpClient = webApplicationFactory.CreateDefaultClient();
    }

    public static HttpClient HttpClient { get; private set; }
}