using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IPatternAreaRepository : IRepository<PatternArea>
    {
        Task<PatternArea> Find(Point p);
    }
}
