using Decidas.Areas.Structure.Features;

namespace Decidas.Areas.Structure;

public static class DependencyInjection
{
    public static IServiceCollection AddAreaStructureServices(this IServiceCollection services)
    {
        services.AddTransient<CreateGroupCommand>();

        services.AddTransient<GetGroupQuery>();
        services.AddTransient<GetGroupListQuery>();

        return services;
    }
}