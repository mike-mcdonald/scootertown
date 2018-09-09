using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class ComplaintRepository : RepositoryBase<Complaint>, IComplaintRepository
    {
        public ComplaintRepository(ScootertownDbContext context) : base(context)
        {
        }

        public override Task<Complaint> Find(string alernateKey)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<Complaint> Add(Complaint item, bool saveImmediately = true)
        {
            await Context.Set<Complaint>().AddAsync(item);
            await Context.Entry(item).Reference(x => x.SubmittedDate).LoadAsync();
            var changes = saveImmediately ? await Context.SaveChangesAsync() : 0;
            return item;
        }

        public async Task<Complaint> Find(DateTime date, string complaintDetails) =>
            await Context.Set<Complaint>().FirstOrDefaultAsync(
                x => x.SubmittedDate.Date == date.Date
                && x.SubmittedTime == date.TimeOfDay
                && x.ComplaintDetails == complaintDetails
            );
    }
}
