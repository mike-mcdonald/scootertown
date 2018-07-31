using PDX.PBOT.Scootertown.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
	public interface IVehicleRepository
	{
		Task<List<Vehicle>> All();
		Task<Vehicle> Find(int vehicleId);
		Task<Vehicle> Add(Vehicle vehicle);
		Task<Vehicle> Update(Vehicle vehicle);
		Task<Vehicle> Find(Guid navManVehicleId);
	}
}
