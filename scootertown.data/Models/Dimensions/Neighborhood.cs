

using System.Collections.Generic;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class Neighborhood : AreaOfInterest
    {
        public Neighborhood(Polygon geometry) : base(geometry) { }

        public List<Deployment> Deployments { get; set; }
        public List<Trip> TripsStarted { get; set; }
        public List<Trip> TripsEnded { get; set; }
    }
}
