using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.SolvingStrategies;
using Explorer.Encounters.Core.EventSourcingDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Explorer.Encounters.Infrastructure.Database
{
    public class EncountersContext: DbContext
    {
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<SocialEncounter> SocialEncounters { get; set; }
        public DbSet<MiscEncounter> MiscEncounters { get; set; }
        public DbSet<HiddenEncounter> HiddenEncounters { get; set; }

        public EncountersContext(DbContextOptions<EncountersContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("encounters");

            modelBuilder.Entity<SocialEncounterEvent>().HasNoKey();

            modelBuilder.Entity<Encounter>().ToTable("Encounters");
            modelBuilder.Entity<Encounter>().Property(x => x.Location).HasColumnType("jsonb");
            modelBuilder.Entity<Encounter>().Property(x => x.Participants).HasColumnType("jsonb");
            modelBuilder.Entity<Encounter>().Property(x => x.Completers).HasColumnType("jsonb");

            modelBuilder.Entity<SocialEncounter>().ToTable("SocialEncounters");
            modelBuilder.Entity<SocialEncounter>().Property(x => x.Location).HasColumnType("jsonb");
            modelBuilder.Entity<SocialEncounter>().Property(x => x.Participants).HasColumnType("jsonb");
            modelBuilder.Entity<SocialEncounter>().Property(x => x.Completers).HasColumnType("jsonb");
            modelBuilder.Entity<SocialEncounter>().Property(x => x.CurrentlyInRange).HasColumnType("jsonb");
            modelBuilder.Entity<SocialEncounter>().Property(x => x.Events).HasColumnType("jsonb");

            modelBuilder.Entity<HiddenEncounter>().ToTable("HiddenEncounters");
            modelBuilder.Entity<HiddenEncounter>().Property(x => x.Location).HasColumnType("jsonb");
            modelBuilder.Entity<HiddenEncounter>().Property(x => x.Participants).HasColumnType("jsonb");
            modelBuilder.Entity<HiddenEncounter>().Property(x => x.Completers).HasColumnType("jsonb");
            modelBuilder.Entity<HiddenEncounter>().Property(x => x.PointLocation).HasColumnType("jsonb");

            modelBuilder.Entity<MiscEncounter>().ToTable("MiscEncounters");
            modelBuilder.Entity<MiscEncounter>().Property(x => x.Location).HasColumnType("jsonb");
            modelBuilder.Entity<MiscEncounter>().Property(x => x.Participants).HasColumnType("jsonb");
            modelBuilder.Entity<MiscEncounter>().Property(x => x.Completers).HasColumnType("jsonb");
        }

    }
}