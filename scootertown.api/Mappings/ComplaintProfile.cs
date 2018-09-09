using AutoMapper;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;

namespace PDX.PBOT.Scootertown.API.Mappings
{
    public class ComplaintProfile : Profile
    {
        public ComplaintProfile()
        {
            CreateMap<ComplaintDTO, Complaint>()
                .ForMember(d => d.SubmittedDate, opt => opt.Ignore())
                .ForMember(d => d.SubmittedTime, opt => opt.MapFrom(s => s.SubmittedDate.TimeOfDay))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location.FromGeoJson<NetTopologySuite.Geometries.Point>()))
                .ForMember(d => d.X, opt => opt.MapFrom(s => s.Location.Coordinates.Longitude))
                .ForMember(d => d.Y, opt => opt.MapFrom(s => s.Location.Coordinates.Latitude));


            CreateMap<Complaint, ComplaintDTO>()
                .ForMember(d => d.SubmittedDate, opt => opt.MapFrom(s => s.SubmittedDate.Date + s.SubmittedTime))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location.ToGeoJson<GeoJSON.Net.Geometry.Point>()));
        }
    }
}
