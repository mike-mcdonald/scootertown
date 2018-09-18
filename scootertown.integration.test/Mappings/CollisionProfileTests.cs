using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.JSON;
using PDX.PBOT.Scootertown.Integration.Mappings;
using PDX.PBOT.Scootertown.Integration.Models;
using Xunit;

namespace PDX.PBOT.Scootertown.Integration.Test.Mappings
{
    public class CollisionProfileTests
    {
        private readonly List<CollisionDTO> Collisions;

        public CollisionProfileTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<CollisionProfile>();
            });
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new SafeGeoJsonConverter());
        }

        [Fact]
        public void ShouldMapBadSample()
        {
            // Given
            string json = @"{
                ""date"": 1533816262,
                ""company_name"": ""Skip"",
                ""device_type"": ""0"",
                ""device_id"": ""M38280DZ"",
                ""other_user"": false,
                ""other_vehicle"": null,
                ""Complaint_id"": ""akdY0x63ieYKuDNXZOe8"",
                ""location"": {
                    ""type"": ""Point"",
                    ""coordinates"": [
                        45.8921374724,
                        -122.90872983
                    ]
                },
                ""helmet"": false,
                ""citation"": false,
                ""citation_details"": null,
                ""injury"": true,
                ""state_report"": false,
                ""claim_status"": null,
                ""reports"": null
            }";

            // When
            var collision = JsonConvert.DeserializeObject<CollisionDTO>(json);

            // Then
            Assert.Null(collision.VehicleTypeKey);
        }
    }
}
