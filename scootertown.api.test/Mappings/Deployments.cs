using AutoMapper;
using PDX.PBOT.Scootertown.API.Mappings;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using Xunit;

namespace PDX.PBOT.Scootertown.API.Test.Mappings
{
    public class Deployments
    {
        public Deployments()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DeploymentProfile>();
            });
        }

        [Fact]
        public void ShouldMapDeployment()
        {
            //Given
            var deployment = new DeploymentDTO
            {
                Location = new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.Position(-45.12638971, 122.54672893563))
            };
            //When
            var dbDeployment = Mapper.Map<Deployment>(deployment);
            //Then
            Assert.NotNull(dbDeployment);
        }
    }
}
