using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories
{
    public abstract class DimensionRepositoryBase<T> : RepositoryBase<T>
        where T : DimensionBase
    {
        protected readonly IDistributedCache Cache;
        public DimensionRepositoryBase(DbContext context, IDistributedCache cache) : base(context)
        {
            Cache = cache;
        }

        public override async Task<T> Find(string name)
        {
            T item = await Context.Set<T>()
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();

            return item;
        }
    }
}
