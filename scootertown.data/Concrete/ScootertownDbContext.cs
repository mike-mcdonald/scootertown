using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PDX.PBOT.Scootertown.Data.Extensions;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Options;

namespace PDX.PBOT.Scootertown.Data.Concrete
{
    public class ScootertownDbContext : DbContext
    {
        readonly VehicleStoreOptions StoreOptions;
        // Bridges
        public DbSet<Models.Bridges.StreetSegmentGroup> BridgeStreetSegmentGroups { get; set; }
        // Dimensions
        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ComplaintType> ComplaintTypes { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<PatternArea> PatternAreas { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PlacementReason> PlacementReasons { get; set; }
        public DbSet<RemovalReason> RemovalReasons { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StreetSegment> StreetSegments { get; set; }
        public DbSet<StreetSegmentGroup> StreetSegmentGroups { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        // Facts
        public DbSet<Collision> Collisions { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Deployment> Deployments { get; set; }


        public ScootertownDbContext(
            DbContextOptions<ScootertownDbContext> options,
            VehicleStoreOptions storeOptions) : base(options)
        {
            this.StoreOptions = storeOptions ?? throw new ArgumentNullException(nameof(storeOptions));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureContext(StoreOptions);
            modelBuilder.SeedData();

            // Override so I can test
            if (!Database.ProviderName.StartsWith("Npgsql"))
            {
                modelBuilder.Entity<Collision>(b => b.Ignore(e => e.Location));
                modelBuilder.Entity<Complaint>(b => b.Ignore(e => e.Location));
                modelBuilder.Entity<Deployment>(b => b.Ignore(e => e.Location));
                modelBuilder.Entity<Trip>(b => b.Ignore(e => e.StartPoint));
                modelBuilder.Entity<Trip>(b => b.Ignore(e => e.EndPoint));
                modelBuilder.Entity<Trip>(b => b.Ignore(e => e.Route));
                modelBuilder.Entity<Neighborhood>(b => b.Ignore(e => e.Geometry));
                modelBuilder.Entity<PatternArea>(b => b.Ignore(e => e.Geometry));
                modelBuilder.Entity<StreetSegment>(b => b.Ignore(e => e.Geometry));
            }

            base.OnModelCreating(modelBuilder);
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
