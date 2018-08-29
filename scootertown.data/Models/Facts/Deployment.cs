using System;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Models.Facts
{
    public class Deployment : ModelBase
    {
        public new long Key { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
        public Point Location { get; set; }
        public sbyte BatteryLevel { get; set; }
        public bool AllowedPlacement { get; set; }
        public bool Reserved { get; set; }
        public bool Disabled { get; set; }

        // Relationships
        public int VehicleKey { get; set; }
        public Vehicle Vehicle { get; set; }
        public int CompanyKey { get; set; }
        public Company Company { get; set; }
        public int VehicleTypeKey { get; set; }
        public VehicleType VehicleType { get; set; }
        public int StartDateKey { get; set; }
        public Calendar StartDate { get; set; }
        public int? EndDateKey { get; set; }
        public Calendar EndDate { get; set; }
        public int? NeighborhoodKey { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public int? PatternAreaKey { get; set; }
        public PatternArea PatternArea { get; set; }
        public int PlacementReasonKey { get; set; }
        public PlacementReason PlacementReason { get; set; }
        public int PickupReasonKey { get; set; }
        public RemovalReason PickupReason { get; set; }
    }
}
