using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class Vehicle
    {
        public int Key { get; set; }
        public string CompanyKey { get; set; }
        public Guid DefaultDriverId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string PWNumber { get; set; }
        public string FleetID { get; set; }
        public string Description { get; set; }
        public string Registration { get; set; }
        public string UniqueVehicleNumber { get; set; }
        public string TypeDisplayName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public List<VehicleLocation> Locations { get; set; }
        public VehicleBureau Bureau { get; set; }
        public VehicleGroup Group { get; set; }
        public VehicleType Type { get; set; }
    }
}