using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Infrastructure.JSON;

namespace PDX.PBOT.Scootertown.Integration.Models
{
    public class CollisionDTO
    {
        [JsonProperty(PropertyName = "date")]
        public long Date { get; set; }
        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }
        [JsonProperty(PropertyName = "device_type")]
        [JsonConverter(typeof(SafeDimensionKeyConverter))]
        public int? VehicleTypeKey { get; set; }
        [JsonProperty(PropertyName = "device_id")]
        public string VehicleName { get; set; }
        [JsonProperty(PropertyName = "other_user")]
        public bool OtherUser { get; set; }
        [JsonProperty(PropertyName = "other_vehicle")]
        [JsonConverter(typeof(SafeDimensionKeyConverter))]
        public int? OtherVehicleTypeKey { get; set; }
        [JsonProperty(PropertyName = "trip_id")]
        public string TripAlternateKey { get; set; }
        [JsonProperty(PropertyName = "location")]
        public Point Location { get; set; }
        [JsonProperty(PropertyName = "helmet")]
        public bool Helmet { get; set; }
        [JsonProperty(PropertyName = "citation")]
        public bool Citation { get; set; }
        [JsonProperty(PropertyName = "citation_details")]
        public string CitationDetails { get; set; }
        [JsonProperty(PropertyName = "injury")]
        public bool Injury { get; set; }
        [JsonProperty(PropertyName = "state_report")]
        public bool StateReport { get; set; }
        [JsonProperty(PropertyName = "claim_status")]
        [JsonConverter(typeof(SafeDimensionKeyConverter))]
        public int? ClaimStatusKey { get; set; }
        [JsonProperty("reports")]
        public string[] Reports { get; set; }
    }
}
