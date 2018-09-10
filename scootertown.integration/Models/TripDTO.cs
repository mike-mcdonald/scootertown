using System;
using System.Runtime.Serialization;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Integration.Models
{
    public class TripDTO
    {
        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }
        [JsonProperty(PropertyName = "device_type")]
        public byte VehicleTypeKey { get; set; }
        [JsonProperty(PropertyName = "device_id")]
        public string VehicleName { get; set; }
        [JsonProperty(PropertyName = "trip_id")]
        public string AlternateKey { get; set; }
        [JsonProperty(PropertyName = "trip_duration")]
        public long Duration { get; set; }
        [JsonProperty(PropertyName = "trip_distance")]
        public long Distance { get; set; }
        [JsonProperty(PropertyName = "start_point")]
        public Point StartPoint { get; set; }
        [JsonProperty(PropertyName = "end_point")]
        public Point EndPoint { get; set; }
        [JsonProperty(PropertyName = "accuracy")]
        public byte Accuracy { get; set; }
        [JsonProperty(PropertyName = "route")]
        public LineString Route { get; set; }
        [JsonProperty(PropertyName = "sample_rate")]
        public short? SampleRate { get; set; }
        [JsonProperty(PropertyName = "start_time")]
        public long StartTime { get; set; }
        [JsonProperty(PropertyName = "end_time")]
        public long EndTime { get; set; }
        [JsonProperty(PropertyName = "parking_verification")]
        public string ParkingVerification { get; set; }
        [JsonProperty(PropertyName = "standard_cost")]
        public int? StandardCost { get; set; }
        [JsonProperty(PropertyName = "actual_cost")]
        public int? ActualCost { get; set; }
        [JsonProperty(PropertyName = "max_speed")]
        public byte? MaxSpeed { get; set; }
        [JsonProperty(PropertyName = "average_speed")]
        public byte? AverageSpeed { get; set; }
        [JsonProperty(PropertyName = "payment_type")]
        public byte PaymentTypeKey { get; set; }
        [JsonProperty(PropertyName = "payment_access")]
        public byte PaymentAccessKey { get; set; }
    }
}
