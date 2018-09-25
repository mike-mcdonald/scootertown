using AutoMapper;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class StreetSegmentProfile : Profile
    {
        const int BUFFER_DISTANCE = 100; // feet
        public StreetSegmentProfile()
        {
            CreateMap<GeoJSON.Net.Feature.Feature, StreetSegment>()
                .ForMember(d => d.AlternateKey, opt => opt.MapFrom(s => s.Properties["objectId"]))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Properties["name"]))
                .ForMember(d => d.Buffer, opt => opt.UseValue(BUFFER_DISTANCE))
                .ForMember(d => d.Geometry, opt => opt.MapFrom(s => s.Geometry.FromGeoJson<Geometry>()))
                .ForMember(d => d.X, opt => opt.MapFrom(s => s.Geometry.FromGeoJson<Geometry>().Centroid.Coordinates[0].X))
                .ForMember(d => d.Y, opt => opt.MapFrom(s => s.Geometry.FromGeoJson<Geometry>().Centroid.Coordinates[0].Y));
        }
    }
}
