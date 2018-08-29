using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class TripProfile : BaseProfile
    {
        public TripProfile() : base()
        {
            CreateMap<Models.TripDTO, API.Models.TripDTO>()
                .ForMember(d => d.Key, opt => opt.Ignore())
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => GetDateTimeFromTimestamp(s.StartTime)))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => GetDateTimeFromTimestamp(s.EndTime)))
                .ForMember(d => d.StartPoint, opt => opt.MapFrom(s => ReadGeoJson<NetTopologySuite.Geometries.Point>(s.StartPoint)))
                .ForMember(d => d.EndPoint, opt => opt.MapFrom(s => ReadGeoJson<NetTopologySuite.Geometries.Point>(s.EndPoint)))
                .ForMember(d => d.Route, opt => opt.MapFrom(s => ReadGeoJson<NetTopologySuite.Geometries.LineString>(s.Route)));
        }
    }
}
