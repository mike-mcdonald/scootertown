using System;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
    public interface IComplaintRepository : IRepository<Complaint>
    {
        Task<Complaint> Find(DateTime date, string complaintDetails);
    }
}
