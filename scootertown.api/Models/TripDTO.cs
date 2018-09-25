using System;
using System.Runtime.Serialization;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.API.Models
{
    public class TripDTO
    {
        public long Key { get; set; }
        public string AlternateKey { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long Duration { get; set; }
        public long Distance { get; set; }
        public Point StartPoint { get; set; }
        public double StartX { get; set; }
        public double StartY { get; set; }
        public Point EndPoint { get; set; }
        public double EndX { get; set; }
        public double EndY { get; set; }
        public LineString Route { get; set; }
        public byte Accuracy { get; set; }
        public short? SampleRate { get; set; }
        public int? StandardCost { get; set; }
        public int? ActualCost { get; set; }
        public byte? MaxSpeed { get; set; }
        public byte? AverageSpeed { get; set; }
        public string ParkingVerification { get; set; }
        public int CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public int? VehicleTypeKey { get; set; }
        public string VehicleTypeName { get; set; }
        public int? VehicleKey { get; set; }
        public string VehicleName { get; set; }
        public int? NeighborhoodStartKey { get; set; }
        public int? NeighborhoodEndKey { get; set; }
        public int? PatternAreaStartKey { get; set; }
        public int? PatternAreaEndKey { get; set; }
        public byte PaymentTypeKey { get; set; }
        public byte PaymentAccessKey { get; set; }
        public long? StreetSegmentGroupKey { get; set; }
    }
}
