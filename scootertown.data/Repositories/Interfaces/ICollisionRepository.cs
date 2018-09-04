using System;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface ICollisionRepository : IRepository<Collision>
    {
        Task<Collision> Find(DateTime date, Point location);
    }
}
