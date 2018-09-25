using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class BicyclePathGroup : ModelBase
    {
        public new long Key { get; set; }
        public Trip Trip { get; set; }
        public ICollection<Bridges.BicyclePathGroup> Bridges { get; set; }
    }
}
