using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class StatusRepository : DimensionRepositoryBase<Status>, IStatusRepository
    {
        public StatusRepository(ScootertownDbContext context) : base(context) { }
    }
}
