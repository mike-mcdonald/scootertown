using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class PaymentTypeRepository : DimensionRepositoryBase<PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(ScootertownDbContext context) : base(context) { }
    }
}
