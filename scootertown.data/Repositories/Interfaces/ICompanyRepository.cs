using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<long> GetTripCount(string companyName);
    }
}
