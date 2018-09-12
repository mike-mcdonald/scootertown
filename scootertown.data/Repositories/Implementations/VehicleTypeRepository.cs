using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class VehicleTypeRepository : DimensionRepositoryBase<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(ScootertownDbContext context, IMemoryCache cache) : base(context, cache)
        {
        }
    }
}
