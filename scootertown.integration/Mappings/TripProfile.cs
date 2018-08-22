using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Integration.Models;
using PDX.PBOT.Scootertown.Integration.Models.Lime;
using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NetTopologySuite.IO;
using NetTopologySuite.Geometries;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class TripProfile : BaseProfile
    {
        public TripProfile() : base()
        {
            CreateMap<Models.TripDTO, Trip>()
                .ForMember(d => d.Key, opt => opt.Ignore())
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => GetTimeSpanFromTimestamp(s.StartTime)))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => GetTimeSpanFromTimestamp(s.EndTime)))
                .ForMember(d => d.StartPoint, opt => opt.MapFrom(s => ReadGeoJson<NetTopologySuite.Geometries.Point>(s.StartPoint)))
                .ForMember(d => d.EndPoint, opt => opt.MapFrom(s => ReadGeoJson<NetTopologySuite.Geometries.Point>(s.EndPoint)))
                .ForMember(d => d.Route, opt => opt.MapFrom(s => ReadGeoJson<NetTopologySuite.Geometries.LineString>(s.Route)))

                // Ignore references, we'll find them later
                .ForMember(d => d.Vehicle, opt => opt.Ignore())
                .ForMember(d => d.Company, opt => opt.Ignore())
                .ForMember(d => d.VehicleType, opt => opt.Ignore())
                .ForMember(d => d.StartDate, opt => opt.Ignore())
                .ForMember(d => d.EndDate, opt => opt.Ignore())
                .ForMember(d => d.PaymentType, opt => opt.Ignore())
                .ForMember(d => d.PaymentAccess, opt => opt.Ignore());

            CreateMap<Models.Lime.TripDTO, Models.TripDTO>()
                .ForMember(d => d.Route, opt => opt.MapFrom(s => (GeoJSON.Net.Geometry.LineString)s.Route));
        }
    }
}
