using Decidas.Areas.Structure.Clients;
using Decidas.Areas.Structure.Policies;

namespace Decidas.Areas.Structure;

public static class Bootstrap
{
    public static IServiceCollection AddStructureModule(this IServiceCollection services)
    {
        // Features
        services.AddTransient<AssignKeeperCommand>();
        services.AddTransient<CreateGroupCommand>();
        services.AddTransient<GetGroupListQuery>();
        services.AddTransient<GetGroupQuery>();

        // Policies
        services.AddTransient<KeepingPolicy>();

        // Clients
        services.AddTransient<PeopleClient>();

        return services;
    }
}
