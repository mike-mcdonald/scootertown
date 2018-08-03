using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using System;
using System.Data.Entity.Spatial;

namespace PDX.PBOT.Scootertown.Data.Models.Facts
{
    public class Deployment
    {
        public long Key { get; set; }
        public string AlternateKey { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DbGeometry Location { get; set; }
        public byte BatteryLevel { get; set; }
        public bool AllowedPlacement { get; set; }
        public bool Reserved { get; set; }
        public bool Disabled { get; set; }

        public Vehicle Vehicle { get; set; }
        public VehicleType VehicleType { get; set; }
        public Company Company { get; set; }
        public Calendar StartDate { get; set; }
        public Calendar EndDate { get; set; }
        public PlacementReason PlacementReason { get; set; }
        public RemovalReason PickupReason { get; set; }
    }
}