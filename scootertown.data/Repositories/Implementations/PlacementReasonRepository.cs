using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class PlacementReasonRepository : DimensionRepositoryBase<PlacementReason>, IPlacementReasonRepository
    {
        public PlacementReasonRepository(ScootertownDbContext context, IMemoryCache cache) : base(context, cache)
        {
        }
    }
}
