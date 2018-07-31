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
    public class SQLVehicleBureauRepository : IVehicleBureauRepository
    {
        private readonly IAmbientDbContextLocator ContextLocator;

        public SQLVehicleBureauRepository(IAmbientDbContextLocator contextLocator)
        {
            ContextLocator = contextLocator;
        }

        public async Task<List<VehicleBureau>> All()
        {
            var context = ContextLocator.Get<NavManDbContext>();

            var types = await context.VehicleBureaus.Select(x => Mapper.Map<VehicleBureau>(x)).ToAsyncEnumerable().ToList();

            return types;
        }

        public async Task<VehicleBureau> Add(VehicleBureau bureau)
        {
            var context = ContextLocator.Get<NavManDbContext>();

            var dbBureau = Mapper.Map<VehicleBureau>(bureau);
            await context.VehicleBureaus.AddAsync(dbBureau);

            await context.SaveChangesAsync();

            return Mapper.Map<VehicleBureau>(dbBureau);
        }

        public async Task<VehicleBureau> Find(byte VehicleBureauId)
        {
            var context = ContextLocator.Get<NavManDbContext>();

            var dbBureau = await context.VehicleBureaus.FindAsync(VehicleBureauId);

            return Mapper.Map<VehicleBureau>(dbBureau);
        }

        public async Task<VehicleBureau> Find(Guid navManVehicleGroupId)
        {
            var context = ContextLocator.Get<NavManDbContext>();

            var dbBureau = await context.VehicleBureaus.Where(t => t.NavManKey == navManVehicleGroupId).ToAsyncEnumerable().FirstOrDefault();

            return Mapper.Map<VehicleBureau>(dbBureau);
        }

        public async Task<VehicleBureau> Update(VehicleBureau bureau)
        {
            var context = ContextLocator.Get<NavManDbContext>();

			var dbBureau = await context.VehicleBureaus.FindAsync(bureau.Key);

			if (dbBureau == null)
			{
                throw new ArgumentException($"Unable to find VehicleBureau with Id: {bureau.Key}");
			}
			// use AutoMapper to update properties on the DB object.
            dbBureau = Mapper.Map<VehicleBureau, VehicleBureau>(bureau, dbBureau);
			
			await context.SaveChangesAsync();

			return Mapper.Map<VehicleBureau>(dbBureau);
        }

        public async Task<VehicleBureau> AddGroup(VehicleBureau bureau, VehicleGroup group)
        {
            var context = ContextLocator.Get<NavManDbContext>();

            var dbBureau = await context.VehicleBureaus.FindAsync(bureau.Key);
			await context.Entry(dbBureau).Collection(x => x.ChildGroups).LoadAsync();
            var dbGroup = await context.VehicleGroups.FindAsync(group.Key);
            await context.Entry(dbGroup).Collection(x => x.Vehicles).LoadAsync();

            dbBureau.ChildGroups.Add(dbGroup);
            dbGroup.Bureau = dbBureau;
            
            foreach(var vehicle in dbGroup.Vehicles)
            {
                vehicle.Bureau = dbBureau;
            }

            await context.SaveChangesAsync();

            return Mapper.Map<VehicleBureau>(dbBureau);
        }
    }
}