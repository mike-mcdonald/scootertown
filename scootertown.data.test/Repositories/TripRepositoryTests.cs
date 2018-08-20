using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Data.Tests.Common;
using Xunit;

namespace PDX.PBOT.Scootertown.Data.Tests.Repositories
{
    [Collection("Database collection")]
    public class TripRepositoryTests
    {
        readonly ITripRepository Repository;
        readonly Trips Trips = new Trips();

        public TripRepositoryTests(DatabaseFixture fixture)
        {
            Repository = new TripRepository(fixture.Context);
        }

        [Fact]
        public async void ShouldSaveTrip()
        {
            //Given
            var trip = Trips[0];
            //When
            trip = await Repository.Add(trip);
            //Then
            Assert.NotNull(trip);
            Assert.Equal(1, trip.Key);
        }
    }
}
