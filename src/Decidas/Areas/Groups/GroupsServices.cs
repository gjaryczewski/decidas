using Decidas.Areas.Groups.Features;

namespace Decidas.Areas.Groups;

public static class GroupsServices
{
    public static IServiceCollection AddGroupsFeatures(this IServiceCollection services)
    {
        services.AddTransient<CreateGroupCommand>();

        return services;
    }
}