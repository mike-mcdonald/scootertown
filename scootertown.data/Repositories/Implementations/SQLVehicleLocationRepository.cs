using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class SQLVehicleLocationRepository : IVehicleLocationRepository
	{
        readonly IAmbientDbContextLocator ContextLocator;

        public SQLVehicleLocationRepository(IAmbientDbContextLocator contextLocator) => ContextLocator = contextLocator;

        public async Task<VehicleLocation> Add(VehicleLocation location)
        {
            var context = ContextLocator.Get<NavManDbContext>();

            if (location.Vehicle != null) context.Attach(location.Vehicle);
            if (location.Bureau != null) context.Attach(location.Bureau);
            if (location.Group != null) context.Attach(location.Group);
            if (location.Type != null) context.Attach(location.Type);
            await context.VehicleLocations.AddAsync(location);

            await context.SaveChangesAsync();

            return location;
        }

        public async Task<List<VehicleLocation>> LocationsForVehicle(int vehicleId, DateTime start, DateTime end)
        {
            var context = ContextLocator.Get<NavManDbContext>();

            var vehicle = await context.Vehicles.FindAsync(vehicleId);

            var locations = vehicle.Locations.Where(l => l.Date.Date.Add(l.Timestamp) >= start && l.Date.Date.Add(l.Timestamp) <= end).ToList();

            return locations;
        }
    }
}