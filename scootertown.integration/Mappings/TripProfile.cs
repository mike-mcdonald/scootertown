using AutoMapper;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Models.TripDTO, API.Models.TripDTO>()
                .ForMember(d => d.Key, opt => opt.Ignore())
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime.ToDateTime()))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime.ToDateTime()))
                .ForMember(d => d.StartX, opt => opt.MapFrom(s => s.StartPoint.Coordinates.Longitude))
                .ForMember(d => d.StartY, opt => opt.MapFrom(s => s.StartPoint.Coordinates.Latitude))
                .ForMember(d => d.EndX, opt => opt.MapFrom(s => s.EndPoint.Coordinates.Longitude))
                .ForMember(d => d.EndY, opt => opt.MapFrom(s => s.EndPoint.Coordinates.Latitude));
        }
    }
}
