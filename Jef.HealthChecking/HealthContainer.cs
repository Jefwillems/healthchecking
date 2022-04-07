using Jef.HealthChecking.Contracts;
using Jef.HealthChecking.Models;

namespace Jef.HealthChecking;

internal class HealthContainer : IHealthContainer
{
    private readonly IDictionary<string, DependencyStatus> _dependencyStatus;

    public HealthContainer()
    {
        _dependencyStatus = new Dictionary<string, DependencyStatus>();
    }

    public void SetHealthStatus(string dependency, HealthStatus level, Exception? exception = null)
    {
        _dependencyStatus[dependency] = new DependencyStatus(exception, level);
    }

    public IDictionary<string, DependencyStatus> GetDependenciesWithStatus()
    {
        return _dependencyStatus;
    }

    public DependencyStatus? GetStatusFor(string name)
    {
        var x = _dependencyStatus.TryGetValue(name, out var ret);
        return x ? ret : null;
    }
}