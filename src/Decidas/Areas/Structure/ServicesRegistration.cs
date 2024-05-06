using Decidas.Areas.Structure.Clients;
using Decidas.Areas.Structure.Features;

namespace Decidas.Areas.Structure;

public static class ServicesRegistration
{
    public static IServiceCollection AddAreaStructureServices(this IServiceCollection services)
    {
        // Commands
        services.AddTransient<AssignKeeperCommand>();
        services.AddTransient<CreateGroupCommand>();

        // Queries
        services.AddTransient<GetGroupQuery>();
        services.AddTransient<GetGroupListQuery>();

        // Events

        // Clients
        services.AddTransient<PeopleClient>();

        return services;
    }
}