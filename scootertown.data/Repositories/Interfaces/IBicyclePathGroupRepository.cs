using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IBicyclePathGroupRepository :
        IRepository<Models.Dimensions.BicyclePathGroup>
    {
        Task<BicyclePathGroup> CreateGroup(IEnumerable<BicyclePath> segments);
        Task<long?> FindGroupKey(string tripAlternateKey);
    }
}
