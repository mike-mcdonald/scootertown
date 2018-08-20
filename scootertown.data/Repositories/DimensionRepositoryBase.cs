using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories
{
    public abstract class DimensionRepositoryBase<T> : RepositoryBase<T>
        where T : DimensionBase
    {
        public DimensionRepositoryBase(DbContext context) : base(context) { }

        public override async Task<T> Find(string name)
        {
            T item = await Context.Set<T>()
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();

            return item;
        }
    }
}
