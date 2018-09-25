using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IStreetSegmentGroupRepository :
        IRepository<Models.Dimensions.StreetSegmentGroup>
    {
        Task<StreetSegmentGroup> CreateGroup(IEnumerable<StreetSegment> segments);
        Task<long?> FindGroupKey(string tripAlternateKey);
    }
}
