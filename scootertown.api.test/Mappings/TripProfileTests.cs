using System;
using AutoMapper;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.API.Mappings;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using Xunit;

namespace PDX.PBOT.Scootertown.API.Test.Mappings
{
    public class TripProfileTests
    {
        public TripProfileTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<TripProfile>();
            });
        }

        [Fact]
        public void ShouldMapTrip()
        {
            //Given
            var trip = new Trip
            {
                Key = 1,
                StartDateKey = 1,
                StartTime = TimeSpan.FromHours(9),
                Route = new NetTopologySuite.Geometries.LineString(new GeoAPI.Geometries.Coordinate[]{
                    new GeoAPI.Geometries.Coordinate(-122.54672893563, 45.12638971),
                    new GeoAPI.Geometries.Coordinate(-122.54672893562, 45.12638972),
                    new GeoAPI.Geometries.Coordinate(-122.54672893561, 45.12638973),
                })
            };
            //When
            var feature = Mapper.Map<Feature>(trip);
            //Then
            Assert.NotNull(feature);
            Assert.Equal(TimeSpan.FromHours(9), feature.Properties["StartTime"]);
        }
    }
}
