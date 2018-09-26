using System;
using System.Threading.Tasks;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Data.Tests.Common;
using Xunit;

namespace PDX.PBOT.Scootertown.Data.Tests.Repositories
{
    [Collection("Database collection")]
    public class BicyclePathGroupRepositoryTests
    {
        private readonly IBicyclePathGroupRepository Repository;
        private readonly IBicyclePathRepository PathRepository;
        private readonly BicyclePaths Paths;

        public BicyclePathGroupRepositoryTests(DatabaseFixture fixture)
        {
            Repository = new BicyclePathGroupRepository(fixture.Context);
            PathRepository = new BicyclePathRepository(fixture.Context);
            Paths = new BicyclePaths();
            PathRepository.Add(Paths[0]);
            PathRepository.Add(Paths[1]);
        }

        [Fact]
        public async void ShouldCreateGroup()
        {
            var now = DateTime.Now;
            //Given

            var paths = new BicyclePath[]{
                Paths[0],
                Paths[1]
            };
            //When
            var group = await Repository.CreateGroup(paths);
            //Then
            Assert.NotNull(group);
            Assert.Equal(1, group.Key);
            Assert.Equal(2, group.Bridges.Count);
        }
    }
}
