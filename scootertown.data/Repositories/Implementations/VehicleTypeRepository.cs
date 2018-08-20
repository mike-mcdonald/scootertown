using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class VehicleTypeRepository : DimensionRepositoryBase<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(ScootertownDbContext context) : base(context) { }
    }
}
