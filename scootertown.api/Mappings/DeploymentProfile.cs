using System;
using AutoMapper;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;

namespace PDX.PBOT.Scootertown.API.Mappings
{
    public class DeploymentProfile : Profile
    {
        public DeploymentProfile() : base()
        {
            CreateMap<Deployment, DeploymentDTO>()
                .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Company.Name))
                .ForMember(d => d.VehicleName, opt => opt.MapFrom(s => s.Vehicle.Name))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location.ToGeoJson<GeoJSON.Net.Geometry.Point>()))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartDate.Date + s.StartTime))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndDate.Date + s.EndTime));

            CreateMap<DeploymentDTO, Deployment>()
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime.TimeOfDay))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime.HasValue ? s.EndTime.Value.TimeOfDay : (TimeSpan?)null))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location.FromGeoJson<NetTopologySuite.Geometries.Point>()))

                // Relationships
                .ForMember(d => d.StartDateKey, opt => opt.Ignore())
                .ForMember(d => d.StartDate, opt => opt.Ignore())
                .ForMember(d => d.EndDateKey, opt => opt.Ignore())
                .ForMember(d => d.EndDate, opt => opt.Ignore())
                .ForMember(d => d.VehicleKey, opt => opt.Ignore())
                .ForMember(d => d.Vehicle, opt => opt.Ignore())
                .ForMember(d => d.CompanyKey, opt => opt.Ignore())
                .ForMember(d => d.Company, opt => opt.Ignore())
                .ForMember(d => d.VehicleType, opt => opt.Ignore())
                .ForMember(d => d.Neighborhood, opt => opt.Ignore())
                .ForMember(d => d.PlacementReason, opt => opt.Ignore())
                .ForMember(d => d.PickupReason, opt => opt.Ignore());
        }
    }
}
