using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class TripRepository : RepositoryBase<Trip>, ITripRepository
    {
        public TripRepository(ScootertownDbContext context) : base(context) { }

        public async Task AddAll(List<Trip> trips)
        {
            Context.Set<Trip>().AddRange(trips);
            await Context.SaveChangesAsync();
        }

        public override async Task<Trip> Add(Trip item, bool saveImmediately = true)
        {
            // make sure we won't violate the indices
            var existingItem = await Context.Set<Trip>().FirstOrDefaultAsync(
                x => (x.AlternateKey == item.AlternateKey)
                || (x.StartDateKey == item.StartDateKey
                && x.VehicleKey == item.VehicleKey
                && x.StartTime == item.StartTime)
            );

            if (existingItem == null)
            {
                await Context.Set<Trip>().AddAsync(item);
                var changes = saveImmediately ? await Context.SaveChangesAsync() : 0;
                return item;
            }
            else
            {
                item.Key = existingItem.Key;
                existingItem = Mapper.Map(item, existingItem);
                return await Update(existingItem);
            }
        }

        public override async Task<Trip> Find(int key) => await Find((long)key);

        public async Task<Trip> Find(long key)
        {
            var trip = await Context.Set<Trip>().FindAsync(key);
            return trip;
        }

        public override async Task<Trip> Find(string companyKey)
        {
            var trip = await Context.Set<Trip>().Where(t => t.AlternateKey == companyKey).FirstOrDefaultAsync();
            return trip;
        }

        public async Task<List<Trip>> Get(DateTime start, DateTime end)
        {
            var trips = await Context.Set<Trip>().Where(t =>
                t.StartDate.Date >= start.Date
                && t.StartDate.Date <= end.Date
            ) // get the broad range
            .Where(t =>
                !(t.StartDate.Date == start.Date
                && t.StartTime <= start.TimeOfDay)
                && !(t.EndDate.Date == end.Date
                && t.EndTime >= end.TimeOfDay)
            ) // eliminate the row before or after the time bounds
            .ToAsyncEnumerable()
            .ToList();

            return trips;
        }

        public override async Task<Trip> Update(Trip item, bool saveImmediately = true)
        {
            var dbItem = await Context.Set<Trip>().FindAsync(item.Key);

            if (dbItem == null)
            {
                throw new ArgumentException($"Unable to find entity with Key: {item.Key}");
            }
            Context.Set<Trip>().Update(dbItem);
            dbItem = Mapper.Map(item, dbItem);

            var changes = saveImmediately ? await Context.SaveChangesAsync() : 0;

            return dbItem;
        }
    }
}
