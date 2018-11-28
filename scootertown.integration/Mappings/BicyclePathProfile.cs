using AutoMapper;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class BicyclePathProfile : Profile
    {
        const int BUFFER_DISTANCE = 100; // feet
        public BicyclePathProfile()
        {
            CreateMap<GeoJSON.Net.Feature.Feature, BicyclePath>()
                .ForMember(d => d.AlternateKey, opt => opt.MapFrom(s => s.Properties["tranPlanID"]))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Properties["name"]))
                .ForMember(d => d.Buffer, opt => opt.UseValue(BUFFER_DISTANCE))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Properties["status"]))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Properties["facility"]))
                .ForMember(d => d.Geometry, opt => opt.MapFrom(s => s.Geometry.FromGeoJson<Geometry>()))
                .ForMember(d => d.X, opt => opt.MapFrom(s => s.Geometry.FromGeoJson<Geometry>().Centroid.Coordinates[0].X))
                .ForMember(d => d.Y, opt => opt.MapFrom(s => s.Geometry.FromGeoJson<Geometry>().Centroid.Coordinates[0].Y));
        }
    }
}
