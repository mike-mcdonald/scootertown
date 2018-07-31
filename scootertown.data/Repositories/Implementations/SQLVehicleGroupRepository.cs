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
    public class SQLVehicleGroupRepository : IVehicleGroupRepository
	{
        readonly IAmbientDbContextLocator ContextLocator;

        public SQLVehicleGroupRepository(IAmbientDbContextLocator contextLocator)
		{
			ContextLocator = contextLocator;
		}

        async Task<VehicleGroup> GetGroup(short groupId)
        {
            var context = ContextLocator.Get<Concrete.NavManDbContext>();

            var group = await context.VehicleGroups.FindAsync(groupId);

            if (group == null)
            {
                throw new ArgumentException($"Couldn't find a VehicleGroup with ID: {groupId}");
            }

            return group;
        }

        async Task<Vehicle> GetVehicle(int vehicleId)
        {
            var context = ContextLocator.Get<Concrete.NavManDbContext>();

            var vehicle = await context.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                throw new ArgumentException($"Couldn't find a vehicle with ID: {vehicleId}");
            }

            return vehicle;
        }

        public async Task<VehicleGroup> Find(short groupId)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
			var dbGroup = await context.VehicleGroups.FindAsync(groupId);

			var group = Mapper.Map<VehicleGroup>(dbGroup);

			return group;
		}

		/// <summary>
		/// Finds the VehicleGroup with the given NavMan Guid
		/// </summary>
		/// <param name="navManVehicleGroupId"></param>
		/// <returns></returns>
		public async Task<VehicleGroup> Find(Guid navManVehicleGroupId, bool includeChildren = false)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
			var dbGroup = await context.VehicleGroups
				.Where(g => g.NavManKey == navManVehicleGroupId)
				.FirstOrDefaultAsync();

			var group = Mapper.Map<VehicleGroup>(dbGroup);
			
			if(includeChildren)
			{
				await context.Entry(dbGroup).Collection(x => x.Children).LoadAsync();
				group.Children = dbGroup.Children.Select(x => Mapper.Map<VehicleGroup>(x)).ToList();
			}

			return group;
		}

		public async Task<VehicleGroup> Add(VehicleGroup group)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
			if(group.Key != 0)
			{
				throw new ArgumentException($"Can't add a VehicleGroup with an ID set.");
			}

			var dbGroup = Mapper.Map<VehicleGroup>(group);

			context.VehicleGroups.Add(dbGroup);
			await context.SaveChangesAsync();

			group = Mapper.Map<VehicleGroup>(dbGroup);
			return group;
		}

		public async Task<List<Vehicle>> GetVehicles(short groupId)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
			var dbGroup = await GetGroup(groupId);

			var vehicles = dbGroup.Vehicles.Select(v => Mapper.Map<Vehicle>(v)).ToList();

			return vehicles;
		}

		public async Task<Vehicle> AddVehicle(short groupId, Vehicle vehicle)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
			var dbGroup = await GetGroup(groupId);

			var dbVehicle = await GetVehicle(vehicle.Key);
			dbGroup.Vehicles.Add(dbVehicle);

			await context.SaveChangesAsync();

			vehicle = Mapper.Map<Vehicle>(dbVehicle);
			return vehicle;
		}

		public async Task<List<VehicleGroup>> All()
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
			var groups = await context.VehicleGroups
				.Include(x => x.Parent)
				.Select(g => Mapper.Map<VehicleGroup>(g))
				.ToListAsync();

			return groups;
		}

		public async Task Delete(short groupId)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
			var dbGroup = await GetGroup(groupId);

			context.VehicleGroups.Remove(dbGroup);
			await context.SaveChangesAsync();
		}

		public async Task<VehicleGroup> Update(VehicleGroup group)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
            var dbGroup = await GetGroup(group.Key);

			context.Update(dbGroup);

			// use AutoMapper to update properties on the DB object.
			dbGroup = Mapper.Map<VehicleGroup, VehicleGroup>(group, dbGroup);

			await context.SaveChangesAsync();

			group = Mapper.Map<VehicleGroup>(dbGroup);
			return group;
		}

		public async Task<VehicleGroup> SetBureau(VehicleGroup group, VehicleBureau bureau)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
			var dbGroup = await GetGroup(group.Key);
			await context.Entry(dbGroup).Reference(x => x.Bureau).LoadAsync();
			var dbBureau = await context.VehicleBureaus.FindAsync(bureau.Key);
			dbGroup.Bureau = dbBureau;

			await context.SaveChangesAsync();

			return Mapper.Map<VehicleGroup>(dbGroup);
		}

		public async Task<VehicleGroup> AddParent(VehicleGroup group, VehicleGroup parent)
		{
			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
			var dbGroup = await GetGroup(group.Key);
			await context.Entry(dbGroup).Reference(x => x.Parent).LoadAsync();
			dbGroup.Parent = await GetGroup(parent.Key);

			await context.SaveChangesAsync();

			return Mapper.Map<VehicleGroup>(dbGroup);
		}

		public async Task<VehicleGroup> AddChild(VehicleGroup parent, VehicleGroup child)
		{

			var context = ContextLocator.Get<Concrete.NavManDbContext>();
			
			var dbGroup = await GetGroup(parent.Key);
			await context.Entry(dbGroup).Reference(x => x.Children).LoadAsync();
			dbGroup.Children.Add(await GetGroup(parent.Key));

			await context.SaveChangesAsync();

			return Mapper.Map<VehicleGroup>(dbGroup);
		}
	}
}