using NetTopologySuite.Geometries;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public abstract class AreaOfInterest : DimensionBase
    {
        public string AlternateKey { get; set; }
        public Polygon Geometry { get; set; }

        public AreaOfInterest(Polygon geometry)
        {
            Geometry = geometry;
        }
    }
}
