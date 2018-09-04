using System;
using AutoMapper;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.API.Mappings;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using Xunit;

namespace PDX.PBOT.Scootertown.API.Test.Mappings
{
    public class CollisionProfileTests
    {
        public CollisionProfileTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<CollisionProfile>();
            });
        }

        [Fact]
        public void ShouldMapCollision()
        {
            //Given
            var now = DateTime.Now;
            var point = new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.Position(-45.12638971, 122.54672893563));
            var Collision = new CollisionDTO
            {
                Date = now,
                Location = point
            };
            //When
            var dbCollision = Mapper.Map<Collision>(Collision);
            //Then
            Assert.NotNull(dbCollision);
            Assert.Equal(now.TimeOfDay, dbCollision.Time);
        }
    }
}
