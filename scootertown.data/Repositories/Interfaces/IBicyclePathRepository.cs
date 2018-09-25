using System.Collections.Generic;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IBicyclePathRepository : IRepository<BicyclePath>
    {
        Task<List<BicyclePath>> Find(LineString line);
    }
}
