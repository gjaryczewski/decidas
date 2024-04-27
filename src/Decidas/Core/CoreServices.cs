using Decidas.Core;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Core;

public static class CoreServices
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDb>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDb")));

        services.AddTransient<DomainEventCollector>();
        services.AddExceptionHandler<DomainErrorHandler>();

        return services;
    }
}