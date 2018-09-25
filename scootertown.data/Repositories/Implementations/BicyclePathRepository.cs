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

        public async Task<List<BicyclePath>> Find(LineString line) =>
            await Context.Set<BicyclePath>()
            .Where(x => x.Geometry.Intersects(line))
            .ToAsyncEnumerable()
            .ToList();

        public override Task<BicyclePath> Find(string alternateKey) =>
            throw new System.NotImplementedException();
    }
}
