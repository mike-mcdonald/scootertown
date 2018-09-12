using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class CompanyRepository : DimensionRepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(ScootertownDbContext context, IMemoryCache cache) : base(context, cache)
        {
        }

        public async Task<long> GetTripCount(string companyName)
        {
            var company = await Find(companyName);

            return await Context.Set<Trip>().Where(x => x.CompanyKey == company.Key).LongCountAsync();
        }
    }
}
