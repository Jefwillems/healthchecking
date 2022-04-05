using Jef.HealthChecking.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HealthChecking.Api.Controllers;

[ApiController]
[Route("home")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHealthCheckRunner _healthCheckRunner;

    public HomeController(ILogger<HomeController> logger, IHealthCheckRunner healthCheckRunner)
    {
        _logger = logger;
        _healthCheckRunner = healthCheckRunner;
    }

    [HttpGet]
    public IActionResult Index(int id = 0)
    {
        return _healthCheckRunner.RunWithHealthCheck("mssql", () =>
        {
            if (id != 0)
            {
                throw new ArgumentException("faulty", nameof(id));
            }

            return Ok("OK");
        });
    }
}