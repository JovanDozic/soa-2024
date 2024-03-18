using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Order;
using Explorer.Payments.Core.Domain.Session;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{
    public DbSet<Payment> Payments { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<TourPurchaseToken> TourPurchaseTokens { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<ShoppingSession> ShoppingSessions { get; set; }


    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");
        modelBuilder.Entity<OrderItem>().HasNoKey();
        modelBuilder.Entity<ShoppingEvent>().HasNoKey();
        modelBuilder.Entity<Payment>().ToTable("Payments");
        modelBuilder.Entity<ShoppingCart>().Property(item => item.Items).HasColumnType("jsonb");
        modelBuilder.Entity<ShoppingSession>().Property(session => session.Events).HasColumnType("jsonb");

        modelBuilder.Entity<Wallet>().ToTable("Wallet");
        modelBuilder.Entity<Sale>().ToTable("Sales");
        modelBuilder.Entity<Sale>()
            .Property(item => item.JsonObject).HasColumnType("jsonb");
    }
}
