using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Personalization;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.Personalization;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Object = Explorer.Tours.Core.Domain.Object;
using Explorer.Tours.Core.Domain.Bundles;

namespace Explorer.Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(ToursProfile).Assembly);
        services.AddAutoMapper(typeof(TourExecutionProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<IProblemService, ProblemService>();
        services.AddScoped<ITouristEquipmentService, TouristEquipmentService>();
        services.AddScoped<IPreferenceService, PreferenceService>();
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<ICampaignService, CampaignService>();
        services.AddScoped<IEquipmentManagmentService, EquipmentManagmentService>();
        services.AddScoped<IObjectService, ObjectService>();
        services.AddScoped<ITouristPositionService, TouristPositionService>();
        services.AddScoped<IPublicRegistrationRequestService, PublicRegistrationRequestService>();
        services.AddScoped<ITourExecutionService, TourExecutionService>();
        services.AddScoped<IBundleService, BundleService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));
        //services.AddScoped(typeof(ICrudRepository<Problem>), typeof(JsonCrudDatabaseRepo<Problem, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TouristEquipment>), typeof(CrudDatabaseRepository<TouristEquipment, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Preference>), typeof(CrudDatabaseRepository<Preference, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(JsonCrudRepo<Tour, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Campaign>), typeof(JsonCrudRepo<Campaign, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<EquipmentManagment>), typeof(CrudDatabaseRepository<EquipmentManagment, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourExecution>), typeof(CrudDatabaseRepository<TourExecution, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Object>), typeof(CrudDatabaseRepository<Object, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<PublicRegistrationRequest>), typeof(CrudDatabaseRepository<PublicRegistrationRequest, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TouristPosition>), typeof(CrudDatabaseRepository<TouristPosition, ToursContext>));
        services.AddScoped(typeof(IPreferenceRepository), typeof(PreferenceRepository));
        services.AddScoped(typeof(IObjectRepository), typeof(ObjectRepository));
        services.AddScoped(typeof(IPublicRegistrationRequestRepository), typeof(PublicRegistrationRequestRepository));
        services.AddScoped(typeof(IProblemRepository), typeof(ProblemRepository));
        services.AddScoped(typeof(ICrudRepository<Bundle>), typeof(JsonCrudRepo<Bundle, ToursContext>));

        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("problems"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "problems")));
    }
}