using System;
using GeoJSON.Net.Geometry;

namespace PDX.PBOT.Scootertown.API.Models
{
    public class ComplaintDTO
    {
        public int Key { get; set; }
        public DateTime SubmittedDate { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
        public Point Location { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public string ComplaintDetails { get; set; }
        public string[] Complaints { get; set; }
        public int? VehicleKey { get; set; }
        public string VehicleName { get; set; }
        public int CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public int? VehicleTypeKey { get; set; }
        public string VehicleTypeName { get; set; }
        public int? ComplaintTypeKey { get; set; }
        public string ComplaintTypeName { get; set; }
    }
}
