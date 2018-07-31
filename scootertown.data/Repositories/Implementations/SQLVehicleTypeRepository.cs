using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EntityFramework.DbContextScope.Interfaces;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class SQLVehicleTypeRepository : IVehicleTypeRepository
    {
        private readonly IAmbientDbContextLocator ContextLocator;

        public SQLVehicleTypeRepository(IAmbientDbContextLocator contextLocator)
        {
            ContextLocator = contextLocator;
        }

        public async Task<List<VehicleType>> All()
        {
            var context = ContextLocator.Get<NavManDbContext>();

            var types = await context.VehicleTypes.Select(x => Mapper.Map<VehicleType>(x)).ToAsyncEnumerable().ToList();

            return types;
        }

        public async Task<VehicleType> Add(VehicleType type)
        {
            var context = ContextLocator.Get<NavManDbContext>();

            var dbType = Mapper.Map<VehicleType>(type);
            await context.VehicleTypes.AddAsync(dbType);

            await context.SaveChangesAsync();

            return Mapper.Map<VehicleType>(dbType);
        }

        public async Task<VehicleType> Find(byte vehicleTypeId)
        {
            var context = ContextLocator.Get<NavManDbContext>();

            var dbType = await context.VehicleTypes.FindAsync(vehicleTypeId);

            return Mapper.Map<VehicleType>(dbType);
        }

        public async Task<VehicleType> Find(Guid navManVehicleTypeId)
        {
            var context = ContextLocator.Get<NavManDbContext>();

            var dbType = await context.VehicleTypes.Where(t => t.NavManKey == navManVehicleTypeId).ToAsyncEnumerable().FirstOrDefault();

            return Mapper.Map<VehicleType>(dbType);
        }

        public async Task<VehicleType> Update(VehicleType type)
        {
            var context = ContextLocator.Get<NavManDbContext>();

			var dbType = await context.VehicleTypes.FindAsync(type.Key);

			if (dbType == null)
			{
				throw new ArgumentException($"Unable to find VehicleType with Id: {type.Key}");
			}
            // never update the name
            // it never comes over from NavMan, so we just set it in the databse directly.
            type.Name = dbType.Name;

			// use AutoMapper to update properties on the DB object.
            dbType = Mapper.Map(type, dbType);
			
			await context.SaveChangesAsync();

			return Mapper.Map<VehicleType>(dbType);
        }
    }
}