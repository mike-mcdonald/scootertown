using AutoMapper;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class DeploymentProfile : Profile
    {
        public DeploymentProfile()
        {
            CreateMap<Models.DeploymentDTO, API.Models.DeploymentDTO>()
                .ForMember(d => d.Key, opt => opt.Ignore())
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime.ToDateTime()))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime.ToDateTime()))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location.FromGeoJson<Point>()))
                .ForMember(d => d.X, opt => opt.MapFrom(s => s.Location.Coordinates.Longitude))
                .ForMember(d => d.Y, opt => opt.MapFrom(s => s.Location.Coordinates.Latitude));
        }
    }
}
