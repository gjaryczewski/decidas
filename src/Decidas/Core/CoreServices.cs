using Decidas.Core;

namespace Decidas.Core;

public static class CoreServices
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<DomainEventCollector>();
        services.AddExceptionHandler<DomainErrorHandler>();
    
        return services;
    }
}