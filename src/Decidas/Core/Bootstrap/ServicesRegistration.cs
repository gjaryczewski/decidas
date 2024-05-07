using Microsoft.EntityFrameworkCore;

namespace Decidas.Core.Bootstrap;

public static class ServicesRegistration
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfigurationManager configuration)
    {
        // Entity Framework
        services.AddDbContext<ApplicationDb>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDb")));

        // Global exceptions
        services.AddExceptionHandler<DomainErrorHandler>();

        return services;
    }
}