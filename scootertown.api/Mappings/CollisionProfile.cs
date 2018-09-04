using AutoMapper;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;

namespace PDX.PBOT.Scootertown.API.Mappings
{
    public class CollisionProfile : Profile
    {
        public CollisionProfile()
        {
            DestinationMemberNamingConvention = new LowerUnderscoreNamingConvention();
            CreateMap<CollisionDTO, Collision>()
                .ForMember(d => d.Date, opt => opt.Ignore())
                .ForMember(d => d.Time, opt => opt.MapFrom(s => s.Date.TimeOfDay))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location.FromGeoJson<NetTopologySuite.Geometries.Point>()))
                .ForMember(d => d.X, opt => opt.MapFrom(s => s.Location.Coordinates.Longitude))
                .ForMember(d => d.Y, opt => opt.MapFrom(s => s.Location.Coordinates.Latitude));


            CreateMap<Collision, CollisionDTO>()
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date.Date + s.Time))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location.ToGeoJson<GeoJSON.Net.Geometry.Point>()));

        }
    }
}
