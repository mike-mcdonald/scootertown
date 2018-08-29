using AutoMapper;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class PatternAreaProfile : Profile
    {
        public PatternAreaProfile()
        {
            CreateMap<GeoJSON.Net.Feature.Feature, PatternArea>()
                .ForMember(d => d.AlternateKey, opt => opt.MapFrom(s => s.Properties["OBJECTID"]))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Properties["Type"]))
                .ForMember(d => d.Geometry, opt => opt.MapFrom(s => s.Geometry.FromGeoJson<Geometry>()));
        }
    }
}
