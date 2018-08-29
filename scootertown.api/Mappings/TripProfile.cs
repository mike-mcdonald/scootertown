using System;
using AutoMapper;
using GeoJSON.Net.Geometry;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;

namespace PDX.PBOT.Scootertown.API.Mappings
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Trip, TripDTO>()
                .ForMember(d => d.Company, opt => opt.MapFrom(s => s.Company.Name))
                .ForMember(d => d.VehicleType, opt => opt.MapFrom(s => s.VehicleType.Name))
                .ForMember(d => d.Vehicle, opt => opt.MapFrom(s => s.Vehicle.Name))
                .ForMember(d => d.StartPoint, opt => opt.MapFrom(s => s.StartPoint.ToGeoJson<GeoJSON.Net.Geometry.Point>()))
                .ForMember(d => d.EndPoint, opt => opt.MapFrom(s => s.EndPoint.ToGeoJson<GeoJSON.Net.Geometry.Point>()))
                .ForMember(d => d.Route, opt => opt.MapFrom(s => s.Route.ToGeoJson<GeoJSON.Net.Geometry.LineString>()))
                .ForMember(d => d.NeighborhoodStart, opt => opt.MapFrom(s => s.NeighborhoodStartKey))
                .ForMember(d => d.NeighborhoodEnd, opt => opt.MapFrom(s => s.NeighborhoodEndKey))
                .ForMember(d => d.PaymentType, opt => opt.MapFrom(s => s.PaymentTypeKey))
                .ForMember(d => d.PaymentAccess, opt => opt.MapFrom(s => s.PaymentAccessKey))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartDate.Date + s.StartTime))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndDate.Date + s.EndTime));

            CreateMap<TripDTO, Trip>()
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime.TimeOfDay))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime.TimeOfDay))
                .ForMember(d => d.StartPoint, opt => opt.MapFrom(s => s.StartPoint.FromGeoJson<NetTopologySuite.Geometries.Point>()))
                .ForMember(d => d.EndPoint, opt => opt.MapFrom(s => s.EndPoint.FromGeoJson<NetTopologySuite.Geometries.Point>()))
                .ForMember(d => d.Route, opt => opt.MapFrom(s => s.Route.FromGeoJson<NetTopologySuite.Geometries.LineString>()))

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
                .ForMember(d => d.NeighborhoodStartKey, opt => opt.MapFrom(s => s.NeighborhoodStart))
                .ForMember(d => d.NeighborhoodStart, opt => opt.Ignore())
                .ForMember(d => d.NeighborhoodEndKey, opt => opt.MapFrom(s => s.NeighborhoodEnd))
                .ForMember(d => d.NeighborhoodEnd, opt => opt.Ignore())
                .ForMember(d => d.PaymentTypeKey, opt => opt.MapFrom(s => s.PaymentType))
                .ForMember(d => d.PaymentType, opt => opt.Ignore())
                .ForMember(d => d.PaymentAccessKey, opt => opt.MapFrom(s => s.PaymentAccess))
                .ForMember(d => d.PaymentAccess, opt => opt.Ignore());
        }
    }
}
