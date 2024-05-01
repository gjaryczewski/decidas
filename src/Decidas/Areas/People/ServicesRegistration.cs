using Decidas.Areas.People.Features;

namespace Decidas.Areas.People;

public static class ServicesRegistration
{
    public static IServiceCollection AddAreaPeopleServices(this IServiceCollection services)
    {
        // Commands
        services.AddTransient<RegisterMemberCommand>();
        services.AddTransient<DesignateKeeperCommand>();

        // Queries
        services.AddTransient<GetMemberQuery>();
        services.AddTransient<GetMemberListQuery>();

        // Events

        return services;
    }
}