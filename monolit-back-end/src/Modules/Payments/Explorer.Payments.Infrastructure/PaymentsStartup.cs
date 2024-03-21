using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Public;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Order;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Session;
using Explorer.Payments.Core.Mappers;
using Explorer.Payments.Core.UseCases.Shopping;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Payments.Infrastructure.Database.Repositories;
using Explorer.Tours.Core.UseCases.Shopping;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Explorer.Payments.Infrastructure;

public static class PaymentsStartup
{
    public static IServiceCollection ConfigurePaymentsModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(PaymentsProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Payment>),
            typeof(CrudDatabaseRepository<Payment, PaymentsContext>));

        services.AddDbContext<PaymentsContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));

        services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, PaymentsContext>));
        services.AddScoped(typeof(ICrudRepository<Wallet>), typeof(CrudDatabaseRepository<Wallet, PaymentsContext>));
        services.AddScoped(typeof(ICrudRepository<Sale>), typeof(JsonCrudRepo<Sale, PaymentsContext>));
        services.AddScoped(typeof(ICrudRepository<TourPurchaseToken>), typeof(CrudDatabaseRepository<TourPurchaseToken, PaymentsContext>));
        services.AddScoped(typeof(ICrudRepository<ShoppingSession>), typeof(CrudDatabaseRepository<ShoppingSession, PaymentsContext>));
        services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
        services.AddScoped(typeof(ITourPurchaseTokenRepository), typeof(TourPurchaseTokenRepository));
        services.AddScoped(typeof(IWalletRepository), typeof(WalletRepository));
        services.AddScoped(typeof(ISaleRepository), typeof(SaleRepository));
        services.AddScoped(typeof(IShoppingSessionRepository), typeof(ShoppingSessionRepository));

        services.AddScoped(typeof(ICrudRepository<Coupon>), typeof(CrudDatabaseRepository<Coupon, PaymentsContext>));


    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ITourPurchaseTokenService, TourPurchaseTokenService>();

        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<ICouponService, CouponService>();

        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<ISaleService, SaleService>();

        services.AddScoped<IShoppingSessionService, ShoppingSessionService>();
    }
}
