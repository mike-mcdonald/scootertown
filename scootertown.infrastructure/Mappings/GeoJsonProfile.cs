using AutoMapper;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;

namespace PDX.PBOT.Scootertown.Infrastructure.Mappings
{
    public class GeoJsonProfile : Profile
    {
        public GeoJsonProfile()
        {
            CreateMap<GeoJSON.Net.Feature.Feature, Neighborhood>()
                .ForMember(d => d.AlternateKey, opt => opt.MapFrom(s => s.Properties["OBJECTID"]))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Properties["MAPLABEL"]))
                .ForMember(d => d.Geometry, opt => opt.MapFrom(s => s.Geometry.FromGeoJson<Geometry>()));

            CreateMap<GeoJSON.Net.Geometry.Polygon, Geometry>()
                .Substitute(s => s.FromGeoJson<Geometry>());

            CreateMap<GeoJSON.Net.Geometry.MultiPolygon, Geometry>()
                .Substitute(s => s.FromGeoJson<Geometry>());

            CreateMap<GeoJSON.Net.Geometry.IGeometryObject, Geometry>()
                .Substitute(s => s.FromGeoJson<Geometry>());

            CreateMap<Point, GeoJSON.Net.Geometry.Point>()
                .Substitute(s => s.ToGeoJson<GeoJSON.Net.Geometry.Point>());

            CreateMap<LineString, GeoJSON.Net.Geometry.LineString>()
                .Substitute(s => s.ToGeoJson<GeoJSON.Net.Geometry.LineString>());
        }
    }
}
