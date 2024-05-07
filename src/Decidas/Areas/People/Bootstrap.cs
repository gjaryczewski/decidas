namespace Decidas.Areas.People;

public static class Bootstrap
{
    public static IServiceCollection AddPeopleModule(this IServiceCollection services)
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
