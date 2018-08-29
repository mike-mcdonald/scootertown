using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class NeighborhoodProfile : BaseProfile
    {
        public NeighborhoodProfile() : base()
        {
            CreateMap<GeoJSON.Net.Feature.Feature, Neighborhood>()
                .ForMember(d => d.AlternateKey, opt => opt.MapFrom(s => s.Properties["OBJECTID"]))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Properties["MAPLABEL"]))
                .ForMember(d => d.Geometry, opt => opt.MapFrom(s => ReadGeoJson<Geometry>(s.Geometry)));
        }
    }
}
