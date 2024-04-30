using Decidas.Areas.People.Features;

namespace Decidas.Areas.People;

public static class DependencyInjection
{
    public static IServiceCollection AddAreaPeopleServices(this IServiceCollection services)
    {
        services.AddTransient<RegisterMemberCommand>();

        return services;
    }
}