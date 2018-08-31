using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class Vehicle : DimensionBase
    {
        public bool Registered { get; set; }

        public Company Company { get; set; }
        public VehicleType Type { get; set; }
        public List<Trip> Trips { get; set; }
        public List<Deployment> Deployments { get; set; }
        public IEnumerable<Complaint> Complaints { get; set; }
    }
}
