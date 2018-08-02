using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class VehicleType
    {
        public byte Key { get; set; }
        public string AlternateKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Vehicle> Vehicles { get; set; }
        public List<Deployment> Deployments { get; set; }
        public List<Trip> Trips { get; set; }
    }
}
