using Jef.HealthChecking.Models;

namespace Jef.HealthChecking.Contracts;

public interface IHealthContainer
{
    void SetHealthStatus(string dependency, HealthStatus level, Exception? exception = null);
    IDictionary<string, DependencyStatus> GetDependenciesWithStatus();
    DependencyStatus? GetStatusFor(string name);
}