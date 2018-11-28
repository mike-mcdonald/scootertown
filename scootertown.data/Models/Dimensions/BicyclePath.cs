using System.Collections.Generic;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class BicyclePath : AreaOfInterest
    {
        public string Status { get; set; }
        public string Type { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int Buffer { get; set; }

        public ICollection<Bridges.BicyclePathGroup> Bridges { get; set; }
    }
}
