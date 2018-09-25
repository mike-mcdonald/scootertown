using System.Collections.Generic;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class StreetSegment : AreaOfInterest
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Buffer { get; set; }

        public ICollection<Bridges.StreetSegmentGroup> Bridges { get; set; }
    }
}
