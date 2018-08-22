using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.Integration.Models.Lime
{
    public class LineStringDTO
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "coordinates")]
        public List<double[]> Coordinates { get; set; }

        public static implicit operator LineString(LineStringDTO l)  // implicit digit to byte conversion operator
        {
            var coordinates = new List<Coordinate>();
            l.Coordinates.ForEach(c =>
            {
                coordinates.Add(new Coordinate(c[0], c[1]));
            });
            return new LineString(coordinates.ToArray());
        }
    }
}
