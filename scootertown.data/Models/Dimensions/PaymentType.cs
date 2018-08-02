using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class PaymentType
    {
        public byte Key { get; set; }
        public string Name { get; set; }
        
        public List<Trip> Trips { get; set; }
    }
}
