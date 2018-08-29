using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Infrastructure.Mappings
{
    public class GeoJsonProfile : BaseProfile
    {
        public GeoJsonProfile() : base()
        {
            CreateMap<GeoJSON.Net.Feature.Feature, Neighborhood>()
                .ForMember(d => d.AlternateKey, opt => opt.MapFrom(s => s.Properties["OBJECTID"]))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Properties["MAPLABEL"]))
                .ForMember(d => d.Geometry, opt => opt.MapFrom(s => ReadGeoJson<Geometry>(s.Geometry)));

            CreateMap<GeoJSON.Net.Geometry.Polygon, Geometry>()
                .Substitute(s => ReadGeoJson<Geometry>(s));

            CreateMap<GeoJSON.Net.Geometry.MultiPolygon, Geometry>()
                .Substitute(s => ReadGeoJson<Geometry>(s));

            CreateMap<GeoJSON.Net.Geometry.IGeometryObject, Geometry>()
                .Substitute(s => ReadGeoJson<Geometry>(s));

            CreateMap<Point, GeoJSON.Net.Geometry.Point>()
                .Substitute(s => WriteGeoJson<GeoJSON.Net.Geometry.Point>(s));

            CreateMap<LineString, GeoJSON.Net.Geometry.LineString>()
                .Substitute(s => WriteGeoJson<GeoJSON.Net.Geometry.LineString>(s));
        }
    }
}
