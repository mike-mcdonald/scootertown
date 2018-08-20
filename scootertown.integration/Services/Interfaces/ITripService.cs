using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Services.Interfaces
{
    public interface ITripService : IService<TripDTO, Trip>
    {
        Task<long> GetTotalTrips(string companyName);
    }
}
