using System;
using GeoJSON.Net.Geometry;

namespace PDX.PBOT.Scootertown.API.Models
{
    public class CollisionDTO
    {
        public int? Key { get; set; }
        public DateTime Date { get; set; }
        public Point Location { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public bool OtherUser { get; set; }
        public bool Helmet { get; set; }
        public bool Injury { get; set; }
        public bool Citation { get; set; }
        public string CitationDetails { get; set; }
        public bool StateReport { get; set; }
        public string[] Reports { get; set; }
        public int? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public int? VehicleKey { get; set; }
        public string VehicleName { get; set; }
        public int? VehicleTypeKey { get; set; }
        public string VehicleTypeName { get; set; }
        public int? OtherVehicleTypeKey { get; set; }
        public string OtherVehicleTypeName { get; set; }
        public long? TripKey { get; set; }
        public string TripAlternateKey { get; set; }
        public int? ClaimStatusKey { get; set; }
        public string ClaimStatusName { get; set; }
        public int? NeighborhoodKey { get; set; }
        public string NeighborhoodName { get; set; }
        public int? PatternAreaKey { get; set; }
        public string PatternAreaName { get; set; }
    }
}
