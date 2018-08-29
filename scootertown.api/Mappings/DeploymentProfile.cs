using System;
using AutoMapper;
using GeoJSON.Net.Geometry;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;

namespace PDX.PBOT.Scootertown.API.Mappings
{
    public class DeploymentProfile : BaseProfile
    {
        public DeploymentProfile() : base()
        {
            CreateMap<Deployment, DeploymentDTO>()
                .ForMember(d => d.Company, opt => opt.MapFrom(s => s.Company.Name))
                .ForMember(d => d.VehicleType, opt => opt.MapFrom(s => s.VehicleType.Name))
                .ForMember(d => d.Vehicle, opt => opt.MapFrom(s => s.Vehicle.Name))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => WriteGeoJson<GeoJSON.Net.Geometry.Point>(s.Location)))
                .ForMember(d => d.Neighborhood, opt => opt.MapFrom(s => s.NeighborhoodKey))
                .ForMember(d => d.PlacementReason, opt => opt.MapFrom(s => s.PlacementReasonKey))
                .ForMember(d => d.PickupReason, opt => opt.MapFrom(s => s.PickupReasonKey))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartDate.Date + s.StartTime))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndDate.Date + s.EndTime));

            CreateMap<DeploymentDTO, Deployment>()
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime.TimeOfDay))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime.HasValue ? s.EndTime.Value.TimeOfDay : (TimeSpan?)null))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => ReadGeoJson<NetTopologySuite.Geometries.Point>(s.Location)))

                // Relationships
                .ForMember(d => d.VehicleKey, opt => opt.Ignore())
                .ForMember(d => d.Vehicle, opt => opt.Ignore())
                .ForMember(d => d.CompanyKey, opt => opt.Ignore())
                .ForMember(d => d.Company, opt => opt.Ignore())
                .ForMember(d => d.VehicleTypeKey, opt => opt.MapFrom(s => s.VehicleType))
                .ForMember(d => d.VehicleType, opt => opt.Ignore())
                .ForMember(d => d.StartDateKey, opt => opt.Ignore())
                .ForMember(d => d.StartDate, opt => opt.Ignore())
                .ForMember(d => d.EndDateKey, opt => opt.Ignore())
                .ForMember(d => d.EndDate, opt => opt.Ignore())
                .ForMember(d => d.NeighborhoodKey, opt => opt.MapFrom(s => s.Neighborhood))
                .ForMember(d => d.Neighborhood, opt => opt.Ignore())
                .ForMember(d => d.PlacementReasonKey, opt => opt.MapFrom(s => s.PlacementReason))
                .ForMember(d => d.PlacementReason, opt => opt.Ignore())
                .ForMember(d => d.PickupReasonKey, opt => opt.MapFrom(s => s.PickupReason))
                .ForMember(d => d.PickupReason, opt => opt.Ignore());
        }
    }
}