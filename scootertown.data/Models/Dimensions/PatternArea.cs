using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class PatternArea : AreaOfInterest
    {
        public List<Deployment> Deployments { get; set; }
        public List<Trip> TripsStarted { get; set; }
        public List<Trip> TripsEnded { get; set; }
    }
}
