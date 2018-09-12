using System;
using System.Threading;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Repositories;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Data.Tests.Common;
using Xunit;

namespace PDX.PBOT.Scootertown.Data.Tests.Repositories
{
    [Collection("Database collection")]
    public class CompanyRepositoryTests
    {
        private readonly ICompanyRepository Repository;
        private readonly Mock<IMemoryCache> MockCache;

        public CompanyRepositoryTests(DatabaseFixture fixture)
        {
            MockCache = new Mock<IMemoryCache>();
            Repository = new CompanyRepository(fixture.Context, MockCache.Object);
        }

        [Fact]
        public void ShouldCallRightPrefix()
        {
        //Given
        
        //When
        
        //Then
        }
    }
}
