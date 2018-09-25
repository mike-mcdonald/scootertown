using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Concrete;
using Bridges = PDX.PBOT.Scootertown.Data.Models.Bridges;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class StreetSegmentGroupRepository : RepositoryBase<StreetSegmentGroup>, IStreetSegmentGroupRepository
    {
        public StreetSegmentGroupRepository(ScootertownDbContext context) : base(context)
        {
        }

        public async Task<StreetSegmentGroup> CreateGroup(IEnumerable<StreetSegment> segments)
        {
            var group = new StreetSegmentGroup();
            await Add(group, true);

            foreach (var segment in segments)
            {
                var bridge = new Bridges.StreetSegmentGroup();
                bridge.StreetSegmentKey = segment.Key;
                bridge.StreetSegmentGroupKey = group.Key;
                await Context.Set<Bridges.StreetSegmentGroup>().AddAsync(bridge);
            }

            await SaveChanges();
            return group;
        }

        public override Task<StreetSegmentGroup> Find(string alternateKey) =>
            throw new System.NotImplementedException();

        public async Task<long?> FindGroupKey(string tripAlternateKey)
        {
            var trip = await Context.Set<Trip>().FirstOrDefaultAsync(t => t.AlternateKey == tripAlternateKey);
            return trip?.StreetSegmentGroupKey;
        }
    }
}
