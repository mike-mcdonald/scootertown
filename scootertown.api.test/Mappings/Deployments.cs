using System;
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
            var now = DateTime.Now;
            var deployment = new DeploymentDTO
            {
                StartTime = now,
                EndTime = now,
                Location = new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.Position(-45.12638971, 122.54672893563))
            };
            //When
            var dbDeployment = Mapper.Map<Deployment>(deployment);
            //Then
            Assert.NotNull(dbDeployment);
            Assert.Equal(now.TimeOfDay, dbDeployment.StartTime);
            Assert.Equal(now.TimeOfDay, dbDeployment.EndTime);
        }
    }
}
