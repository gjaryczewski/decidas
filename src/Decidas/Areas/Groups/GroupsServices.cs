using Decidas.Areas.Groups.Features;
using Decidas.Core;

namespace Decidas.Areas.Groups;

public static class GroupsServices
{
    public static IServiceCollection AddGroupsFeatures(this IServiceCollection services)
    {
        services.AddTransient<CreateGroupCommand>();
        services.AddTransient<DomainEventCollector>();

        return services;
    }
}