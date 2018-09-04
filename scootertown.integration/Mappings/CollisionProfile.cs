using AutoMapper;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class CollisionProfile : Profile
    {
        public CollisionProfile()
        {
            CreateMap<Models.CollisionDTO, API.Models.CollisionDTO>()
                .ForMember(d => d.Key, opt => opt.Ignore())
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date.ToDateTime()))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location))
                .ForMember(d => d.X, opt => opt.MapFrom(s => s.Location.Coordinates.Longitude))
                .ForMember(d => d.Y, opt => opt.MapFrom(s => s.Location.Coordinates.Latitude));
        }
    }
}
