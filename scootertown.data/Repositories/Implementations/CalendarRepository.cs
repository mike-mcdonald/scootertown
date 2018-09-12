using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class CalendarRepository : DimensionRepositoryBase<Calendar>, ICalendarRepository
    {
        public CalendarRepository(ScootertownDbContext context, IMemoryCache cache)
         : base(context, cache) { }

        public async Task<Calendar> Add(DateTime date)
        {
            Calendar calendar = new Calendar(date);

            await Context.Set<Calendar>().AddAsync(calendar);
            await Context.SaveChangesAsync();
            return calendar;
        }

        public override async Task<Calendar> Find(string name)
        {
            var date = Convert.ToDateTime(name);
            return await Find(date);
        }

        public async Task<Calendar> Find(DateTime date)
        {
            Calendar item = null;
            var cacheKey = $"{CachePrefix}:{date.Date.ToShortDateString().ToLower()}";

            var cachedKey = await Cache.GetOrCreateAsync(cacheKey, async (entry) =>
            {
                item = await Context.Set<Calendar>()
                    .ToAsyncEnumerable()
                    .FirstOrDefault(c => c.Date == date.Date);

                entry.SetSize(1);
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(60));
                return item?.Key;
            });

            return cachedKey.HasValue ? new Calendar { Key = cachedKey.Value } : null;
        }
    }
}
