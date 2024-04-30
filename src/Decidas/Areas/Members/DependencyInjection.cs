using Decidas.Areas.Members.Features;

namespace Decidas.Areas.Members;

public static class DependencyInjection
{
    public static IServiceCollection AddAreaMembersServices(this IServiceCollection services)
    {
        services.AddTransient<RegisterMemberCommand>();

        return services;
    }
}