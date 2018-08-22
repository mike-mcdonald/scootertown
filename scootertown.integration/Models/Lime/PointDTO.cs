using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.Integration.Models.Lime
{
    public class PointDTO
    {
        private readonly double x;
        private readonly double y;

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "coordinates")]
        public double[] Coordinates { get; set; }

        public PointDTO(Point p)
        {
            Type = "point";
            x = p.X;
            y = p.Y;
        }

        public static explicit operator PointDTO(Point p)  // explicit byte to digit conversion operator
        {
            PointDTO l = new PointDTO(p);  // explicit conversion
            return l;
        }

        public static implicit operator Point(PointDTO l)  // implicit digit to byte conversion operator
        {
            Point p = new Point(x: l.Coordinates[0], y: l.Coordinates[1]);
            System.Console.WriteLine("conversion occurred");
            return p;  // implicit conversion
        }
    }
}
