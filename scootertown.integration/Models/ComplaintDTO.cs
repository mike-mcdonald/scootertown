using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.Integration.Models
{
    public class ComplaintDTO
    {
        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }
        [JsonProperty(PropertyName = "device_type")]
        public byte? VehicleTypeKey { get; set; }
        [JsonProperty(PropertyName = "device_id")]
        public string VehicleName { get; set; }
        [JsonProperty(PropertyName = "date_submitted")]
        public long? SubmittedDate { get; set; }
        [JsonProperty(PropertyName = "location")]
        public Point Location { get; set; }
        [JsonProperty(PropertyName = "complaint_details")]
        public string ComplaintDetails { get; set; }
        [JsonProperty(PropertyName = "complaints")]
        public string[] Complaints { get; set; }
        [JsonProperty(PropertyName = "complaint_type")]
        public int? ComplaintTypeKey { get; set; }
    }
}
