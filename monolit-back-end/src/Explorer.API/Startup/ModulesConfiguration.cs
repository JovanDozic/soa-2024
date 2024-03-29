using Explorer.Blog.Infrastructure;
using Explorer.Encounters.Infrastructure;
using Explorer.Stakeholders.Infrastructure;
using Explorer.Tours.Infrastructure;
using Explorer.Payments.Infrastructure;

namespace Explorer.API.Startup;

public static class ModulesConfiguration
{
    public static IServiceCollection RegisterModules(this IServiceCollection services)
    {
        services.ConfigureStakeholdersModule();
        services.ConfigureBlogModule();
        services.ConfigureToursModule();
        services.ConfigureEncountersModule();
        services.ConfigurePaymentsModule();
        return services;
    }
}