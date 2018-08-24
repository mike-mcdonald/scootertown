using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers.Interfaces
{
    public interface ICompanyManager
    {
        string Company { get; }
        long StartingOffset { set; }
        Task<Queue<DeploymentDTO>> RetrieveAvailability();
        Task<Queue<TripDTO>> RetrieveTrips(long offset = 0);
        Task<Queue<CollisionDTO>> RetrieveCollisions();
        Task<Queue<ComplaintDTO>> RetrieveComplaints();
    }
}
