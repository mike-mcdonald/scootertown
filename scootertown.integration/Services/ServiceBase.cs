using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Integration.Infrastructure;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration.Services
{
    public abstract class ServiceBase<TSource, TDest> : IService<TSource, TDest>
        where TDest : ModelBase
    {
        protected readonly ICalendarRepository CalendarRepository;

        protected ServiceBase(ICalendarRepository calendarRepository)
        {
            CalendarRepository = calendarRepository;
        }
        public abstract Task Save(List<TSource> items);

        protected async Task<Calendar> FindOrAddCalendar(long timestamp)
        {
            System.DateTime date = (new DateTime()).FromUnixTimestamp(timestamp);
            return await FindOrAddCalendar(date);
        }

        protected async Task<Calendar> FindOrAddCalendar(DateTime date)
        {
            var dbItem = await CalendarRepository.Find(date);

            if (dbItem == null)
            {
                dbItem = await CalendarRepository.Add(date);
            }

            return dbItem;
        }

        protected async Task<T> FindOrAdd<T>(IRepository<T> repository, string alternateKey, T item)
        {
            var dbItem = await repository.Find(alternateKey);

            if (dbItem == null)
            {
                dbItem = await repository.Add(item);
            }

            return dbItem;
        }

        protected async Task<T> FindOrAdd<T>(IRepository<T> repository, int key, T item)
        {
            var dbItem = await repository.Find(key);

            if (dbItem == null)
            {
                dbItem = await repository.Add(item);
            }

            return dbItem;
        }
    }
}
