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
    public class PatternAreaRepository : DimensionRepositoryBase<PatternArea>, IPatternAreaRepository
    {
        public PatternAreaRepository(ScootertownDbContext context, IMemoryCache cache) : base(context, cache)
        {
        }

        public override async Task<PatternArea> Add(PatternArea item, bool saveImmediately)
        {
            var existingItem = await Context.Set<PatternArea>().Where(n => n.Geometry == item.Geometry).FirstOrDefaultAsync();

            var dbItem = existingItem ?? (await Context.Set<PatternArea>().AddAsync(item)).Entity;

            var count = saveImmediately ? await Context.SaveChangesAsync() : 0;

            return dbItem;
        }

        public async Task<PatternArea> Find(Point point) =>
            await Context.Set<PatternArea>()
                .Where(n => n.Geometry.Contains(point))
                .FirstOrDefaultAsync();
    }
}
