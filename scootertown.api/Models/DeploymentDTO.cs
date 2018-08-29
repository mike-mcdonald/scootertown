using System;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.API.Models
{
    public class DeploymentDTO
    {
        public long Key { get; set; }
        public string Company { get; set; }
        public byte VehicleType { get; set; }
        public string Vehicle { get; set; }
        public sbyte BatteryLevel { get; set; }
        public Point Location { get; set; }
        public int? Neighborhood { get; set; }
        public bool InEastPortland { get; set; }
        public byte PlacementReason { get; set; }
        public bool AllowedPlacement { get; set; }
        public byte PickupReason { get; set; }
        public bool Reserved { get; set; }
        public bool Disabled { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
    }
}
