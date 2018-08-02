using System;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PDX.PBOT.Scootertown.Data.Extensions;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Options;

namespace PDX.PBOT.Scootertown.Data.Concrete
{
	public class ScootertownDbContext : DbContext
	{
		readonly VehicleStoreOptions StoreOptions;
		public DbSet<VehicleType> VehicleTypes { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<Calendar> Calendar { get; set; }
        public DbSet<Trip> Trips { get; set; }


		public ScootertownDbContext(VehicleStoreOptions storeOptions) : base()
		{
            this.StoreOptions = storeOptions ?? throw new ArgumentNullException(nameof(storeOptions));
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.ConfigureContext(StoreOptions);

            base.OnModelCreating(modelBuilder);
		}

		public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
	}
}