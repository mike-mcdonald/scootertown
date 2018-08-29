using PDX.PBOT.Scootertown.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface ITripRepository : IRepository<Trip>
    {
        Task<long> CountByCompany(string company);
        Task AddAll(List<Trip> trips);
        Task<Trip> Find(long key);
        Task<List<Trip>> Get(DateTime start, DateTime end);
    }
}
