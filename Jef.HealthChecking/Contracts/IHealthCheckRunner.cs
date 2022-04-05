using Jef.HealthChecking.Models;

namespace Jef.HealthChecking.Contracts;

public interface IHealthCheckRunner
{
    TReturnType RunWithHealthCheck<TReturnType>(
        string dependency,
        Func<TReturnType> action,
        HealthStatus statusIfFail = HealthStatus.CRIT);
}