using Decidas.Core;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDb>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDb")));

        services.AddExceptionHandler<DomainErrorHandler>();

        return services;
    }
}