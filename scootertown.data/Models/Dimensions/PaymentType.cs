using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class PaymentType : DimensionBase
    {
        public List<Trip> TripsPayType { get; set; }
        public List<Trip> TripsPayAccess { get; set; }
    }
}
