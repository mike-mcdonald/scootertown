using System;
using System.Runtime.Serialization;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.API.Models
{
    public class TripDTO
    {
        public long Key { get; set; }
        public string Company { get; set; }
        public byte VehicleType { get; set; }
        public string Vehicle { get; set; }
        public string AlternateKey { get; set; }
        public long Duration { get; set; }
        public long Distance { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public LineString Route { get; set; }
        public byte Accuracy { get; set; }
        public short? SampleRate { get; set; }
        public int? NeighborhoodStart { get; set; }
        public int? NeighborhoodEnd { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ParkingVerification { get; set; }
        public int? StandardCost { get; set; }
        public int? ActualCost { get; set; }
        public byte? MaxSpeed { get; set; }
        public byte? AverageSpeed { get; set; }
        public byte PaymentType { get; set; }
        public byte PaymentAccess { get; set; }
    }
}
