using GeoJSON.Net.Geometry;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.Integration.Models.Lime
{
    public class PointDTO
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "coordinates")]
        public double[] Coordinates { get; set; }

        public PointDTO() { }

        public static implicit operator NetTopologySuite.Geometries.Point(PointDTO l)  // implicit digit to byte conversion operator
        {
            NetTopologySuite.Geometries.Point p = new NetTopologySuite.Geometries.Point(x: l.Coordinates[0], y: l.Coordinates[1]);
            return p;  // implicit conversion
        }

        public static implicit operator GeoJSON.Net.Geometry.Point(PointDTO l)  // implicit digit to byte conversion operator
        {
            GeoJSON.Net.Geometry.Point p = new GeoJSON.Net.Geometry.Point(new Position(l.Coordinates[1], l.Coordinates[0]));
            return p;  // implicit conversion
        }
    }
}
