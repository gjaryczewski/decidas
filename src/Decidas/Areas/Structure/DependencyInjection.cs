using Decidas.Areas.Structure.Features;

namespace Decidas.Areas.Structure;

public static class DependencyInjection
{
    public static IServiceCollection AddAreaStructureServices(this IServiceCollection services)
    {
        services.AddTransient<CreateGroupCommand>();

        services.AddTransient<GetGroupDetailsQuery>();
        services.AddTransient<GetGroupListQuery>();

        return services;
    }
}