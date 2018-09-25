using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.JSON;
using PDX.PBOT.Scootertown.Integration.Mappings;
using PDX.PBOT.Scootertown.Integration.Models;
using Xunit;

namespace PDX.PBOT.Scootertown.Integration.Test.Mappings
{
    public class StreetSegmentProfileTests
    {
        public StreetSegmentProfileTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<StreetSegmentProfile>();
            });
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new SafeGeoJsonConverter());
        }

        [Fact]
        public void ShouldMapSample()
        {
            // given
            var geometry = new Point(new Position(45.597714, -122.651975));
            var feature = new Feature(geometry, new
            {
                name = "NE 13TH AVE",
                leftadd1 = 10201,
                leftadd2 = 10499,
                rgtadd1 = 10200,
                rgtadd2 = 10498,
                objectId = 1
            });
            // when
            var segment = Mapper.Map<StreetSegment>(feature);
            // then
            Assert.Equal(feature.Properties["name"], segment.Name);
            Assert.Equal(feature.Properties["objectId"].ToString(), segment.AlternateKey);
            Assert.Equal(100, segment.Buffer);
        }
    }
}
