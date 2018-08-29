using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IDeploymentRepository : IRepository<Deployment>
    {
        Task AddAll(List<Deployment> deployments);
        Task<Deployment> Find(long key);
        Task<List<Deployment>> Get(DateTime start, DateTime end);
        Task<List<Deployment>> GetActive();
        Task<List<Deployment>> GetActive(string companyName);
    }
}
