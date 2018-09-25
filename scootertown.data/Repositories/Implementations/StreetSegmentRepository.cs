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
    public class StreetSegmentRepository : RepositoryBase<StreetSegment>, IStreetSegmentRepository
    {
        public StreetSegmentRepository(ScootertownDbContext context) : base(context)
        {
        }

        public async Task<List<StreetSegment>> Find(LineString line) =>
            await Context.Set<StreetSegment>()
            .Where(x => x.Geometry.Intersects(line))
            .ToAsyncEnumerable()
            .ToList();

        public override Task<StreetSegment> Find(string alternateKey) =>
            throw new System.NotImplementedException();
    }
}
