using System.Net.Mime;
using Jef.HealthChecking.Contracts;
using Jef.HealthChecking.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jef.HealthChecking.Controllers;

[Produces("application/json")]
[Route("status")]
[ApiController]
public class HealthController : ControllerBase
{
    private readonly IHealthContainer _healthContainer;

    public HealthController(IHealthContainer healthContainer)
    {
        _healthContainer = healthContainer;
    }

    [HttpGet("am-i-up")]
    public IActionResult AmIUp()
    {
        return Content("OK", MediaTypeNames.Text.Plain);
    }

    [HttpGet("aggregate")]
    public IActionResult Aggregate()
    {
        var results = _healthContainer.GetDependenciesWithStatus();
        var currentLevel = HealthStatus.OK;
        var messages = new List<object>();
        foreach (var (depName, (exception, healthStatus)) in results)
        {
            if (healthStatus > currentLevel)
            {
                currentLevel = healthStatus;
            }

            if (healthStatus > HealthStatus.OK)
            {
                // dependency is not ok, add to messages
                messages.Add(new
                {
                    description = $"Dependency {depName} is not healthy",
                    result = healthStatus == HealthStatus.WARN ? "WARN" : "CRIT",
                    details = exception?.Message ?? $"an error occured executing healthcheck for {depName}"
                });
            }
        }


        messages.Insert(0, GetHealthString(currentLevel));

        return Ok(messages);
    }

    [HttpGet("aggregate/{name}")]
    public IActionResult GetDependencyStatus(string name)
    {
        var status = _healthContainer.GetStatusFor(name);

        return Ok(GetHealthString(status?.Level ?? HealthStatus.OK));
    }

    private string GetHealthString(HealthStatus status)
    {
        return status switch
        {
            HealthStatus.OK => "OK",
            HealthStatus.WARN => "WARN",
            HealthStatus.CRIT => "CRIT",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}