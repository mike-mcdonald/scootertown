using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers.Interfaces
{
    public interface ICompanyManager
    {
        string Company { get; }
        long StartingOffset { set; }
        Task<List<DeploymentDTO>> RetrieveAvailability();
        Task<List<TripDTO>> RetrieveTrips(long offset = 0);
        Task<List<CollisionDTO>> RetrieveCollisions();
        Task<List<ComplaintDTO>> RetrieveComplaints();
    }
}
