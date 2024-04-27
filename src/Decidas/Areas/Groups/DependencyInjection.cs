using Decidas.Areas.Groups.Features;

namespace Decidas.Areas.Groups;

public static class DependencyInjection
{
    public static IServiceCollection AddAreaGroupsServices(this IServiceCollection services)
    {
        services.AddTransient<CreateGroupCommand>();

        services.AddTransient<GetGroupQuery>();

        return services;
    }
}