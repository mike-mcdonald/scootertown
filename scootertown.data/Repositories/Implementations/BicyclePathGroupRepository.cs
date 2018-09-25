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
    public class BicyclePathGroupRepository : RepositoryBase<BicyclePathGroup>, IBicyclePathGroupRepository
    {
        public BicyclePathGroupRepository(ScootertownDbContext context) : base(context)
        {
        }

        public async Task<BicyclePathGroup> CreateGroup(IEnumerable<BicyclePath> paths)
        {
            var group = new BicyclePathGroup();
            await Add(group, true);

            foreach (var path in paths)
            {
                var bridge = new Bridges.BicyclePathGroup();
                bridge.BicyclePathKey = path.Key;
                bridge.BicyclePathGroupKey = group.Key;
                await Context.Set<Bridges.BicyclePathGroup>().AddAsync(bridge);
            }

            await SaveChanges();
            return group;
        }

        public override Task<BicyclePathGroup> Find(string alternateKey) =>
            throw new System.NotImplementedException();

        public async Task<long?> FindGroupKey(string tripAlternateKey)
        {
            var trip = await Context.Set<Trip>().FirstOrDefaultAsync(t => t.AlternateKey == tripAlternateKey);
            return trip?.BicyclePathGroupKey;
        }
    }
}
