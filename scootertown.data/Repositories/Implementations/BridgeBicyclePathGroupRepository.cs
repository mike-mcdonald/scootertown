using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Bridges;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class BridgeBicyclePathGroupRepository : RepositoryBase<BicyclePathGroup>, IBridgeBicyclePathGroupRepository
    {
        public BridgeBicyclePathGroupRepository(ScootertownDbContext context) : base(context)
        {
        }

        public override Task<BicyclePathGroup> Find(string alternateKey) =>
            throw new System.NotImplementedException();
    }
}
