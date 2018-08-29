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
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime.ToDateTime()));
        }
    }
}
