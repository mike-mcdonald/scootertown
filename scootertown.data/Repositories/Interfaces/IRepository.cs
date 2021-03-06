﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Queryable();
        Task<T> Add(T item, bool saveImmediately = true);
        Task<List<T>> All();
        Task<long> Count();
        Task<T> Find(int key);
        Task<T> Find(string alternateKey);
        Task<T> Update(T item, bool saveImmediately = true);
        Task SaveChanges();
    }
}
