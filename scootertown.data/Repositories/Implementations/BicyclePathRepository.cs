using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class BicyclePathRepository : RepositoryBase<BicyclePath>, IBicyclePathRepository
    {
        public BicyclePathRepository(ScootertownDbContext context) : base(context)
        {
        }

        public override async Task<BicyclePath> Add(BicyclePath item, bool saveImmediately)
        {
            var existingItem = await Context.Set<BicyclePath>().Where(n => n.Geometry == item.Geometry).FirstOrDefaultAsync();

            if (existingItem == null)
            {
                await Context.Set<BicyclePath>().AddAsync(item);
                var changes = saveImmediately ? await Context.SaveChangesAsync() : 0;
                return item;
            }
            else
            {
                item.Key = existingItem.Key;
                return await Update(item);
            }
        }

        public async Task<List<BicyclePath>> Find(LineString line) =>
            await Context.Set<BicyclePath>()
            .Where(x => x.Geometry.Intersects(line))
            .ToAsyncEnumerable()
            .ToList();

        public override Task<BicyclePath> Find(string alternateKey) =>
            throw new System.NotImplementedException();
    }
}
