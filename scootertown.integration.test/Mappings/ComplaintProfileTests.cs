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
    public class ComplaintProfileTests
    {
        private readonly List<ComplaintDTO> Complaints;

        public ComplaintProfileTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ComplaintProfile>();
            });
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new SafeGeoJsonConverter());
        }

        [Fact]
        public void ShouldMapComplaintWithIncompleteInfo()
        {
            //Given
            string json = @"{
                ""company_name"": ""Bird"",
                ""device_type"": 1,
                ""date_submitted"": 1536764040,
                ""location"": {
                    ""type"": ""Point"",
                    ""coordinates"": [
                        -122.64,
                        45.47
                    ]
                },
                ""complaint_type"": 8,
                ""complaint_details"": ""Vehicle component issue"",
                ""complaints"": []
            }";
            //When
            var complaint = JsonConvert.DeserializeObject<ComplaintDTO>(json);
            //Then
            Assert.NotNull(complaint);
        }
    }
}
