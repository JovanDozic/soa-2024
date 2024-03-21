using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserFollowerService, UserFollowerService>();
        services.AddScoped<IUserNotificationService, UserNotificationService>();
        services.AddScoped<IAppRatingService, AppRatingService>();
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<IClubInvitationService, ClubInvitationService>();
        services.AddScoped<IMembershipRequestService, MembershipRequestService>();
        services.AddScoped<IClubMemberService, ClubMemberService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IInternalPersonService, InternalPersonService>();

    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, StakeholdersContext>));

        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, StakeholdersContext>));

        services.AddScoped(typeof(ICrudRepository<ClubInvitation>), typeof(CrudDatabaseRepository<ClubInvitation, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<AppRating>), typeof(CrudDatabaseRepository<AppRating, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<MembershipRequest>),
            typeof(CrudDatabaseRepository<MembershipRequest, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<ClubMember>), typeof(CrudDatabaseRepository<ClubMember, StakeholdersContext>));

        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));
    }
}