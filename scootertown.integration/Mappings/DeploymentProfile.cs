using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class DeploymentProfile : BaseProfile
    {
        public DeploymentProfile() : base()
        {
            CreateMap<Models.DeploymentDTO, API.Models.DeploymentDTO>()
                .ForMember(d => d.Key, opt => opt.Ignore())
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => GetDateTimeFromTimestamp(s.StartTime)))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => GetDateTimeFromTimestamp(s.EndTime)))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => ReadGeoJson<Point>(s.Location)));
        }
    }
}
