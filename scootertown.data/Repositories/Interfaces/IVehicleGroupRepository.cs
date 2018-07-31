using PDX.PBOT.Scootertown.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
	public interface IVehicleGroupRepository
	{
		Task<VehicleGroup> Find(short vehicleGroupId);
		Task<VehicleGroup> Find(Guid navManVehicleGroupId, bool includeChildren = false);
		Task<List<VehicleGroup>> All();
		Task<VehicleGroup> Add(VehicleGroup group);
		Task<VehicleGroup> Update(VehicleGroup group);
		Task<VehicleGroup> AddParent(VehicleGroup group, VehicleGroup parent);
		Task<VehicleGroup> AddChild(VehicleGroup parent, VehicleGroup child);
		Task Delete(short groupId);
		Task<Vehicle> AddVehicle(short groupId, Vehicle vehicle);
		Task<List<Vehicle>> GetVehicles(short groupId);
	}
}