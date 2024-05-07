using Microsoft.EntityFrameworkCore;

namespace Decidas.Core;

public static class Bootstrap
{
    public static IServiceCollection AddCoreModule(this IServiceCollection services, IConfigurationManager configuration)
    {
        // Entity Framework
        services.AddDbContext<ApplicationDb>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDb")));

        // Domain error handler
        services.AddExceptionHandler<DomainErrorHandler>();

        return services;
    }
}
