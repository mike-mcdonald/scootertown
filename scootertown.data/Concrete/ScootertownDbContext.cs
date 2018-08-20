using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Extensions;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Options;

namespace PDX.PBOT.Scootertown.Data.Concrete
{
    public class ScootertownDbContext : DbContext
    {
        readonly VehicleStoreOptions StoreOptions;
        // Dimensions
        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<VehicleType> Companies { get; set; }
        public DbSet<VehicleType> PaymentTypes { get; set; }
        public DbSet<VehicleType> PlacementReasons { get; set; }
        public DbSet<VehicleType> RemovalReasons { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        // Facts
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Deployment> Deployments { get; set; }


        public ScootertownDbContext(DbContextOptions<ScootertownDbContext> options, VehicleStoreOptions storeOptions) : base(options)
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
                modelBuilder.Entity<Deployment>(b => b.Ignore(e => e.Location));
                modelBuilder.Entity<Trip>(b => b.Ignore(e => e.StartPoint));
                modelBuilder.Entity<Trip>(b => b.Ignore(e => e.EndPoint));
                modelBuilder.Entity<Trip>(b => b.Ignore(e => e.Route));
            }

            base.OnModelCreating(modelBuilder);
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
