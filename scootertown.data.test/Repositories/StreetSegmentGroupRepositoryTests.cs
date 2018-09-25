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
    public class StreetSegmentGroupRepositoryTests
    {
        private readonly IStreetSegmentGroupRepository Repository;
        private readonly IStreetSegmentRepository SegmentRepository;
        private readonly StreetSegments Segments;

        public StreetSegmentGroupRepositoryTests(DatabaseFixture fixture)
        {
            Repository = new StreetSegmentGroupRepository(fixture.Context);
            SegmentRepository = new StreetSegmentRepository(fixture.Context);
            Segments = new StreetSegments();
            SegmentRepository.Add(Segments[0]);
            SegmentRepository.Add(Segments[1]);
        }

        [Fact]
        public async void ShouldCreateGroup()
        {
            var now = DateTime.Now;
            //Given

            var segments = new StreetSegment[]{
                Segments[0],
                Segments[1]
            };
            //When
            var group = await Repository.CreateGroup(segments);
            //Then
            Assert.NotNull(group);
            Assert.Equal(1, group.Key);
            Assert.Equal(2, group.Bridges.Count);
        }
    }
}
