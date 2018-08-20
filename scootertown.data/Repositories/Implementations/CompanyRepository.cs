using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class CompanyRepository : DimensionRepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(ScootertownDbContext context) : base(context) { }

        public async Task<long> GetTripCount(string companyName)
        {
            var count = await Context.Set<Company>()
                .Where(x => x.Name == companyName)
                .LongCountAsync();
            return count;
        }
    }
}
