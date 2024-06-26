using HabitTracker.Api.IntegrationTests.Infrastructure;
using Xunit;

namespace HabitTracker.Api.IntegrationTests.Specs;

public class HealthCheckTests :  IntegrationTestBase
{
    [Fact]
    public async Task HealthCheckReturns200()
    {
        var result = await HttpClient.GetAsync("healthCheck");

        result.EnsureSuccessStatusCode();
    }
}