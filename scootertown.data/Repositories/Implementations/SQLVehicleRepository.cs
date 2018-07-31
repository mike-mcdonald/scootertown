using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class SQLVehicleRepository : IVehicleRepository
	{
		private readonly IAmbientDbContextLocator ContextLocator;

		public SQLVehicleRepository(IAmbientDbContextLocator contextLocator)
		{
			ContextLocator = contextLocator;
		}

		public async Task<Vehicle> Add(Vehicle vehicle)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();

            await context.Vehicles.AddAsync(vehicle);
			await context.SaveChangesAsync();

            return vehicle;
		}

        public async Task<List<Vehicle>> All()
        {
            var context = ContextLocator.Get<Concrete.NavManDbContext>();

            var vehicles = await context.Vehicles
                .Include(v => v.Group)
                .Include(v => v.Type)
				.Include(v => v.Bureau)
                .ToListAsync();

            return vehicles;
        }

		public async Task<Vehicle> Find(int vehicleId)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();

			var vehicle = await context.Vehicles.FindAsync(vehicleId);

			if (vehicle != null)
			{
				await context.Entry(vehicle)
					.Reference(b => b.Type)
					.LoadAsync();

				await context.Entry(vehicle)
					.Reference(b => b.Group)
					.LoadAsync();
				
				await context.Entry(vehicle)
					.Reference(b => b.Bureau)
					.LoadAsync();
			}

			return vehicle;
		}

		public async Task<Vehicle> Find(Guid navManVehicleId)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();

			var vehicle = await context.Vehicles.Where(v => v.NavManKey == navManVehicleId)
				.FirstOrDefaultAsync();

			if (vehicle != null)
			{
				await context.Entry(vehicle)
					.Reference(b => b.Type)
					.LoadAsync();

				await context.Entry(vehicle)
					.Reference(b => b.Group)
					.LoadAsync();
					
				await context.Entry(vehicle)
					.Reference(b => b.Bureau)
					.LoadAsync();
			}

			return vehicle;
		}

		public async Task<Vehicle> Update(Vehicle vehicle)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();

			var dbVehicle = await context.Vehicles.FindAsync(vehicle.Key);

			if (dbVehicle == null)
			{
				throw new ArgumentException($"Unable to find Vehicle with Id: {vehicle.Key}");
			}
			// use AutoMapper to update properties on the DB object.
			dbVehicle = Mapper.Map(vehicle, dbVehicle);

			await context.SaveChangesAsync();

			vehicle = dbVehicle;

			return vehicle;
		}
	}
}
