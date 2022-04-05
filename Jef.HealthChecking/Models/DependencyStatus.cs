namespace Jef.HealthChecking.Models;

public readonly record struct DependencyStatus(Exception? Exception, HealthStatus Level)
{
    public Exception? Exception { get; } = Exception;
    public HealthStatus Level { get; } = Level;
}