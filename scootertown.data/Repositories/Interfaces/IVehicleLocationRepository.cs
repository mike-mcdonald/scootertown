using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IVehicleLocationRepository
    {
        Task<List<VehicleLocation>> LocationsForVehicle(int vehicleId, DateTime start, DateTime end);
        Task<VehicleLocation> Add(VehicleLocation location);
    }
}
