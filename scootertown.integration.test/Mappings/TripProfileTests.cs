using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Integration.Infrastructure;
using PDX.PBOT.Scootertown.Integration.Mappings;
using PDX.PBOT.Scootertown.Integration.Models;
using Xunit;

namespace PDX.PBOT.Scootertown.Integration.Test.Mappings
{
    public class TripProfileTests
    {
        private readonly List<TripDTO> Trips;

        public TripProfileTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<TripProfile>();
            });
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new SafeGeoJsonConverter());

            Trips = JsonConvert.DeserializeAnonymousType(File.ReadAllText("sample.trips.json"), new { trips = new List<TripDTO>() }, settings).trips;
        }

        [Fact]
        public void ShouldMapSample()
        {
            var trips = new List<Trip>();

            foreach (var trip in Trips)
            {
                trips.Add(Mapper.Map<Trip>(trip));
            }
        }
    }
}
