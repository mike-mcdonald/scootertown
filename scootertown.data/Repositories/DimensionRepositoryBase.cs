using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories
{
    public abstract class DimensionRepositoryBase<T> : RepositoryBase<T>
        where T : DimensionBase, new()
    {
        protected readonly IMemoryCache Cache;
        public DimensionRepositoryBase(ScootertownDbContext context, IMemoryCache cache) : base(context)
        {
            Cache = cache;
        }

        protected virtual string CachePrefix
        {
            get
            {
                return this.GetType().Name
                    .Split(new string[] { "Repository" }, StringSplitOptions.RemoveEmptyEntries)
                    .First()
                    .ToLower();
            }
        }
        public override async Task<T> Add(T item, bool saveImmediately)
        {
            item = await base.Add(item, saveImmediately);
            var cacheKey = $"{CachePrefix}:{item.Name.ToLower()}";
            Cache.Set(cacheKey, item.Key, new MemoryCacheEntryOptions
            {
                Size = 1,
                SlidingExpiration = TimeSpan.FromSeconds(60)
            });
            return item;
        }

        public override async Task<T> Find(string name)
        {
            T item = null;
            var cacheKey = $"{CachePrefix}:{name.ToLower()}";

            var cachedKey = await Cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                item = await Context.Set<T>()
                    .Where(x => x.Name == name)
                    .FirstOrDefaultAsync();

                entry.SetSize(1);
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(60));
                return item?.Key;
            });

            return cachedKey.HasValue ? new T { Key = cachedKey.Value } : null;
        }
    }
}
