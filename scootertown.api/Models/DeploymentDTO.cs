using System;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.API.Models
{
    public class DeploymentDTO
    {
        public long Key { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
        public sbyte BatteryLevel { get; set; }
        public Point Location { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public bool AllowedPlacement { get; set; }
        public bool Reserved { get; set; }
        public bool Disabled { get; set; }
        public int? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public int? VehicleKey { get; set; }
        public string VehicleName { get; set; }
        public byte VehicleTypeKey { get; set; }
        public string VehicleTypeName { get; set; }
        public int? NeighborhoodKey { get; set; }
        public int? PatternAreaKey { get; set; }
        public string PatternAreaName { get; set; }
        public byte PlacementReasonKey { get; set; }
        public byte PickupReasonKey { get; set; }
    }
}
