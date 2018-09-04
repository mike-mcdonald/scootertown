using System;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Data.Tests.Common;
using Xunit;

namespace PDX.PBOT.Scootertown.Data.Tests.Repositories
{
    [Collection("Database collection")]
    public class CollisionRepositoryTests
    {
        private readonly ICollisionRepository Repository;
        private readonly Collisions Collisions;

        public CollisionRepositoryTests(DatabaseFixture fixture)
        {
            Repository = new CollisionRepository(fixture.Context);
            Collisions = new Collisions();
        }

        [Fact]
        public async void ShouldSaveCollision()
        {
            var now = DateTime.Now;
            //Given
            var collision = Collisions[0];
            //When
            collision = await Repository.Add(collision);
            //Then
            Assert.NotNull(collision);
            Assert.Equal(1, collision.Key);
        }

        [Fact]
        public async void ShouldUpdateSameCollision()
        {
            //Given
            var collision = Collisions[0];
            //When
            await Repository.Add(collision);
            var count = await Repository.Count();
            await Repository.Add(collision);
            //Then
            Assert.Equal(count, await Repository.Count());
        }

        [Fact]
        public async void ShouldUpdateCollision()
        {
            //Given
            var collision = Collisions[0];
            collision.Helmet = true;
            //When
            await Repository.Add(collision);
            collision.Helmet = false;
            await Repository.Update(collision);
            collision = await Repository.Find(1);
            //Then
            Assert.Equal(1, await Repository.Count());
            Assert.Equal(false, collision.Helmet);
        }

        [Fact]
        public async void ShouldFindCollision()
        {
            //Given
            var collision = Collisions[0];
            var date = collision.Date.Date + collision.Time;
            var location = collision.Location = new Point(new Coordinate(-122.34567, 45.56789));
            //When
            await Repository.Add(collision);
            collision = await Repository.Find(date, location);
            //Then
            Assert.NotNull(collision);
            Assert.Equal(1, collision.Key);
        }
    }
}
