using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class Vehicle
    {
        public int Key { get; set; }
        public string AlternateKey { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Company Company { get; set; }
        public VehicleType Type { get; set; }
        public List<Trip> Trips { get; set; }
        public List<Deployment> Deployments { get; set; }
    }
}