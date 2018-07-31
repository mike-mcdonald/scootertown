using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IVehicleTypeRepository
    {
        Task<List<VehicleType>> All();
        Task<VehicleType> Find(byte vehicleTypeId);
        Task<VehicleType> Find(Guid navManVehicleTypeId);
        Task<VehicleType> Add(VehicleType type);
        Task<VehicleType> Update(VehicleType type);
    }
}