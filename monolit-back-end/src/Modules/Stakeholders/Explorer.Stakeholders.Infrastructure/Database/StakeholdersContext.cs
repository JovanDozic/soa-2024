using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<AppRating> AppRatings { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<ClubInvitation> ClubInvitations { get; set; }
    public DbSet<ClubMember> ClubMembers { get; set; }

    public DbSet<MembershipRequest> MembershipRequests { get; set; }    

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        modelBuilder.Entity<User>().Property(u => u.Followers).HasColumnType("jsonb");
        modelBuilder.Entity<User>().Property(u => u.Notifications).HasColumnType("jsonb");

        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);
    }
}