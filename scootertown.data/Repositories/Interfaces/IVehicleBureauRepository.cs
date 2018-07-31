using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IVehicleBureauRepository
    {
        Task<List<VehicleBureau>> All();
        Task<VehicleBureau> Find(byte vehicleBureauId);
        Task<VehicleBureau> Find(Guid navManVehicleGroupId);
        Task<VehicleBureau> Add(VehicleBureau bureau);
        Task<VehicleBureau> Update(VehicleBureau bureau);
        Task<VehicleBureau> AddGroup(VehicleBureau bureau, VehicleGroup group);
    }
}