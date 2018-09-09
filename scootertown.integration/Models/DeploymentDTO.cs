using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.Integration.Models
{
    public class DeploymentDTO
    {
        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }
        [JsonProperty(PropertyName = "device_type")]
        public byte VehicleTypeKey { get; set; }
        [JsonProperty(PropertyName = "device_id")]
        public string VehicleName { get; set; }
        [JsonProperty(PropertyName = "battery_level")]
        public sbyte BatteryLevel { get; set; }
        [JsonProperty(PropertyName = "location")]
        public Point Location { get; set; }
        [JsonProperty(PropertyName = "placement_reason")]
        public byte PlacementReasonKey { get; set; }
        [JsonProperty(PropertyName = "allowed_placement")]
        public bool AllowedPlacement { get; set; }
        [JsonProperty(PropertyName = "pickup_reason")]
        public byte PickupReasonKey { get; set; }
        [JsonProperty(PropertyName = "is_reserved")]
        public bool Reserved { get; set; }
        [JsonProperty(PropertyName = "is_disabled")]
        public bool Disabled { get; set; }
        [JsonProperty(PropertyName = "availability_start_time")]
        public int StartTime { get; set; }
        [JsonProperty(PropertyName = "availability_end_time")]
        public int? EndTime { get; set; }
    }
}
