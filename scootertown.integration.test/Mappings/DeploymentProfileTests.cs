using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.JSON;
using PDX.PBOT.Scootertown.Integration.Mappings;
using PDX.PBOT.Scootertown.Integration.Models;
using Xunit;

namespace PDX.PBOT.Scootertown.Integration.Test.Mappings
{
    public class DeploymentProfileTests
    {
        private readonly List<DeploymentDTO> Deployments;

        public DeploymentProfileTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DeploymentProfile>();
            });
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new SafeGeoJsonConverter());

            Deployments = JsonConvert.DeserializeAnonymousType(File.ReadAllText("sample.deployments.json"), new List<DeploymentDTO>(), settings);
        }

        [Fact]
        public void ShouldMapSample()
        {
            var deployments = new List<API.Models.DeploymentDTO>();

            foreach (var deployment in Deployments)
            {
                deployments.Add(Mapper.Map<API.Models.DeploymentDTO>(deployment));
            }
        }

        [Fact]
        public void ShouldParseXY()
        {
            //Given
            var deployment = Deployments.FirstOrDefault(x => x.Location != null);
            var location = deployment.Location;
            //When
            var apiDeployment = Mapper.Map<API.Models.DeploymentDTO>(deployment);
            //Then
            Assert.Equal(location.Coordinates.Longitude, apiDeployment.X);
            Assert.Equal(location.Coordinates.Latitude, apiDeployment.Y);
        }
    }
}
