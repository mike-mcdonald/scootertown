using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class NeighborhoodRepository : DimensionRepositoryBase<Neighborhood>, INeighborhoodRepository
    {
        public NeighborhoodRepository(ScootertownDbContext context, IMemoryCache cache) : base(context, cache)
        {
        }

        public override async Task<Neighborhood> Add(Neighborhood item, bool saveImmediately)
        {
            var existingItem = await Context.Set<Neighborhood>().Where(n => n.Geometry == item.Geometry).FirstOrDefaultAsync();

            var dbItem = existingItem ?? (await Context.Set<Neighborhood>().AddAsync(item)).Entity;

            var count = saveImmediately ? await Context.SaveChangesAsync() : 0;

            return dbItem;
        }

        public async Task<Neighborhood> Find(Point point) =>
            await Context.Set<Neighborhood>()
                .Where(n => n.Geometry.Contains(point))
                .FirstOrDefaultAsync();
    }
}
