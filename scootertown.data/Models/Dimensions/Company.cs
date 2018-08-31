using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class Company : DimensionBase
    {
        public List<Vehicle> Vehicles { get; set; }
        public List<Deployment> Deployments { get; set; }
        public List<Trip> Trips { get; set; }
        public IEnumerable<Complaint> Complaints { get; set; }
    }
}
