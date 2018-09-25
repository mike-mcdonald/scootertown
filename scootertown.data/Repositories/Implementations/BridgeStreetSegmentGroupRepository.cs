using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Bridges;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class BridgeStreetSegmentGroupRepository : RepositoryBase<StreetSegmentGroup>, IBridgeStreetSegmentGroupRepository
    {
        public BridgeStreetSegmentGroupRepository(ScootertownDbContext context) : base(context)
        {
        }

        public override Task<StreetSegmentGroup> Find(string alternateKey) =>
            throw new System.NotImplementedException();
    }
}
