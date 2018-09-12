using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class CollisionRepository : RepositoryBase<Collision>, ICollisionRepository
    {
        public CollisionRepository(ScootertownDbContext context) : base(context)
        {
        }

        public override Task<Collision> Find(string alernateKey)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<Collision> Add(Collision item, bool saveImmediately = true)
        {
            await Context.Set<Collision>().AddAsync(item);
            await Context.Entry(item).Reference(x => x.Date).LoadAsync();
            var changes = saveImmediately ? await Context.SaveChangesAsync() : 0;
            return item;
        }

        public async Task<Collision> Find(DateTime date, Point location) =>
            await Context.Set<Collision>().FirstOrDefaultAsync(
                x => x.Date.Date == date.Date
                && x.Time == date.TimeOfDay
                && x.Location == location
            );
    }
}
