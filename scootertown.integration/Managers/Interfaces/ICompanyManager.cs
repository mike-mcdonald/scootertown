using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers.Interfaces
{
    public interface ICompanyManager
    {
        string Company { get; }
        long Offset { get; set; }
        Task<Queue<DeploymentDTO>> RetrieveAvailability();
        Task<Queue<TripDTO>> RetrieveTrips();
        Task<Queue<CollisionDTO>> RetrieveCollisions();
        Task<Queue<ComplaintDTO>> RetrieveComplaints();
    }
}
