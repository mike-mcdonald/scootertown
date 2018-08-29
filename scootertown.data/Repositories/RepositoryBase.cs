using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Models;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : ModelBase
    {
        protected readonly DbContext Context;

        public RepositoryBase(DbContext context)
        {
            Context = context;
        }

        public virtual async Task<T> Add(T item, bool saveImmediately)
        {
            var dbItem = await Context.Set<T>().AddAsync(item);

            var changes = saveImmediately ? await Context.SaveChangesAsync() : 0;

            return dbItem.Entity;
        }

        public virtual async Task<List<T>> All() => await Context.Set<T>().ToAsyncEnumerable().ToList();
        public virtual async Task<long> Count() => await Context.Set<T>().ToAsyncEnumerable().LongCount();

        public virtual async Task<T> Find(int key)
        {
            var item = await Context.Set<T>().FindAsync(key);

            return item;
        }

        public abstract Task<T> Find(string alernateKey);

        public virtual async Task<T> Update(T item, bool saveImmediately = true)
        {
            var dbItem = await Context.Set<T>().FindAsync(item.Key);

            if (dbItem == null)
            {
                throw new ArgumentException($"Unable to find entity with Key: {item.Key}");
            }

            dbItem = Mapper.Map(item, dbItem);

            var changes = saveImmediately ? await Context.SaveChangesAsync() : 0;

            return item;
        }

        public virtual async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }
    }
}
