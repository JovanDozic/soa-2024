using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.EventSourcingDomain;
using Explorer.Encounters.Core.Mappers;
using Explorer.Encounters.Core.UseCases;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Encounters.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Explorer.Encounters.Infrastructure
{
    public static class EncountersStartup
    {
        public static IServiceCollection ConfigureEncountersModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EncounterProfile).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);

            return services;
        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<Encounter>),
                typeof(CrudDatabaseRepository<Encounter, EncountersContext>));
            services.AddScoped(typeof(ISocialEncounterRepository), typeof(SocialEncounterRepository));
            services.AddScoped(typeof(IHiddenEncounterRepository), typeof(HiddenEncounterRepository));
            services.AddScoped(typeof(IMiscEncounterRepository), typeof(MiscEncounterRepository));



            services.AddDbContext<EncountersContext>(opt =>
                opt.UseNpgsql(DbConnectionStringBuilder.Build("encounters"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "encounters")));

        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IEncounterService, EncounterService>();
            services.AddScoped<ISocialEncounterService, SocialEncounterService>();
            services.AddScoped<IHiddenEncounterService, HiddenEncounterService>();
            services.AddScoped<IMiscEncounterService, MiscEncounterService>();
        }
    }
}
