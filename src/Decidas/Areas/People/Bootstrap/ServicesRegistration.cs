using Decidas.Areas.People;

namespace Decidas.Areas.People.Bootstrap;

public static class ServicesRegistration
{
    public static IServiceCollection AddAreaPeople(this IServiceCollection services)
    {
        // Features
        services.AddTransient<DesignateKeeperCommand>();
        services.AddTransient<GetKeeperDetailsCommand>();
        services.AddTransient<GetKeeperListQuery>();
        services.AddTransient<GetMemberListQuery>();
        services.AddTransient<GetMemberQuery>();
        services.AddTransient<RegisterMemberCommand>();

        return services;
    }
}