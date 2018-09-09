using AutoMapper;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class ComplaintProfile : Profile
    {
        public ComplaintProfile()
        {
            CreateMap<Models.ComplaintDTO, API.Models.ComplaintDTO>()
                .ForMember(d => d.SubmittedDate, opt => opt.MapFrom(s => s.SubmittedDate.ToDateTime()))
                .ForMember(d => d.X, opt => opt.MapFrom(s => s.Location.Coordinates.Longitude))
                .ForMember(d => d.Y, opt => opt.MapFrom(s => s.Location.Coordinates.Latitude));
        }
    }
}
