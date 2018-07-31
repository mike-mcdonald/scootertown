using System;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Extensions;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Options;

namespace PDX.PBOT.Scootertown.Data.Concrete
{
	public class ScootertownDbContext : DbContext, IDbContext
	{
		readonly VehicleStoreOptions storeOptions;
		public DbSet<VehicleBureau> VehicleBureaus { get; set; }
		public DbSet<VehicleGroup> VehicleGroups { get; set; }
		public DbSet<VehicleType> VehicleTypes { get; set; }
		public DbSet<VehicleLocation> VehicleLocations { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<Calendar> Calendar { get; set; }

		public ScootertownDbContext(DbContextOptions<ScootertownDbContext> options, VehicleStoreOptions storeOptions) : base(options)
		{
            this.storeOptions = storeOptions ?? throw new ArgumentNullException(nameof(storeOptions));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ConfigureContext(storeOptions);

            base.OnModelCreating(modelBuilder);
		}

		public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
	}
}