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
    public class DeploymentRepository : RepositoryBase<Deployment>, IDeploymentRepository
    {
        public DeploymentRepository(ScootertownDbContext context) : base(context) { }

        public override async Task<Deployment> Add(Deployment item, bool saveImmediately = true)
        {
            // make sure we won't violate the index
            var existingItem = await Context.Set<Deployment>().FirstOrDefaultAsync(
                x => x.StartDateKey == item.StartDateKey
                && x.VehicleKey == item.VehicleKey
                && x.StartTime == item.StartTime
            );

            if (existingItem == null)
            {
                await Context.Set<Deployment>().AddAsync(item);
                var changes = saveImmediately ? await Context.SaveChangesAsync() : 0;
                return item;
            }
            else
            {
                existingItem.LastSeen = item.LastSeen;
                return await Update(existingItem);
            }
        }

        public async Task AddAll(List<Deployment> deployments)
        {
            Context.Set<Deployment>().AddRange(deployments);
            await Context.SaveChangesAsync();
        }

        public override async Task<Deployment> Find(int key) => await Find((long)key);

        public async Task<Deployment> Find(long key) =>
            await Context.Set<Deployment>().FindAsync(key);

        public override Task<Deployment> Find(string companyKey) =>
            throw new NotImplementedException();

        public async Task<List<Deployment>> Get(DateTime start, DateTime end) =>
            await Context.Set<Deployment>().Where(t =>
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

        public override async Task<Deployment> Update(Deployment item, bool saveImmediately = true)
        {
            var dbItem = await Context.Set<Deployment>().FindAsync(item.Key);

            if (dbItem == null)
            {
                throw new ArgumentException($"Unable to find entity with Key: {item.Key}");
            }
            Context.Set<Deployment>().Update(dbItem);
            dbItem = Mapper.Map(item, dbItem);

            var changes = saveImmediately ? await Context.SaveChangesAsync() : 0;

            return dbItem;
        }

        public async Task<List<Deployment>> GetActive() =>
            await Context.Set<Deployment>()
                .Include(x => x.Vehicle)
                .Include(x => x.Company)
                .Include(x => x.StartDate)
                .Include(x => x.EndDate)
                .Where(x => x.EndDateKey == null)
                .ToAsyncEnumerable()
                .ToList();

        public async Task<List<Deployment>> GetActive(string companyName) =>
            await Context.Set<Deployment>()
                .Include(x => x.Vehicle)
                .Include(x => x.Company)
                .Include(x => x.StartDate)
                .Include(x => x.EndDate)
                .Where(x => x.EndDateKey == null)
                .Where(x => x.Company.Name == companyName)
                .ToAsyncEnumerable()
                .ToList();
    }
}
