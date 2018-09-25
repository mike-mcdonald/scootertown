using System.Collections.Generic;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IStreetSegmentRepository : IRepository<StreetSegment>
    {
        Task<List<StreetSegment>> Find(LineString line);
    }
}
