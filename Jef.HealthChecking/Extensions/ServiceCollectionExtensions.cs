using Jef.HealthChecking.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Jef.HealthChecking.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHealthChecking(this IServiceCollection services)
    {
        services.AddScoped<IHealthCheckRunner, HealthCheckRunner>();
        services.AddSingleton<IHealthContainer, HealthContainer>();
        services.AddMvcCore().AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly);
        return services;
    }
}