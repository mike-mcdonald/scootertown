using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface INeighborhoodRepository : IRepository<Neighborhood>
    {
        Task<Neighborhood> Find(Point p);
    }
}
