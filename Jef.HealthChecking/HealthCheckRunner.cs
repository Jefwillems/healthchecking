using Jef.HealthChecking.Contracts;
using Jef.HealthChecking.Models;
using Microsoft.Extensions.Logging;

namespace Jef.HealthChecking;

internal class HealthCheckRunner : IHealthCheckRunner
{
    private readonly ILogger<HealthCheckRunner> _logger;
    private readonly IHealthContainer _healthContainer;

    public HealthCheckRunner(ILogger<HealthCheckRunner> logger, IHealthContainer healthContainer)
    {
        _logger = logger;
        _healthContainer = healthContainer;
    }

    public TReturnType RunWithHealthCheck<TReturnType>(
        string dependency,
        Func<TReturnType> action,
        HealthStatus statusIfFail = HealthStatus.CRIT)
    {
        try
        {
            var result = action();
            _logger.LogDebug("healthcheck for {Dependency} succeeded", dependency);
            _healthContainer.SetHealthStatus(dependency, HealthStatus.OK);
            return result;
        }
        catch (Exception e)
        {
            _healthContainer.SetHealthStatus(dependency, statusIfFail, e);
            _logger.LogDebug(e, "healthcheck runner for {Dependency} failed", dependency);
            throw;
        }
    }
}