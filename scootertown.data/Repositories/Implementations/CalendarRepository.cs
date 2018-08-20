using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly ScootertownDbContext Context;

        public CalendarRepository(ScootertownDbContext context)
        {
            Context = context;
        }

        public async Task<Calendar> Add(DateTime date)
        {
            Calendar calendar = new Calendar(date);

            await Context.Calendar.AddAsync(calendar);
            await Context.SaveChangesAsync();
            return calendar;
        }

        public async Task<Calendar> Find(DateTime date)
        {
            var calendar = await Context.Calendar.ToAsyncEnumerable().FirstOrDefault(c => c.Date == date.Date);

            return calendar;
        }
    }
}
