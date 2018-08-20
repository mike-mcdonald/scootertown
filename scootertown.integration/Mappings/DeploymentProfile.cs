using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Integration.Models;
using GeoJSON.Net;
using Newtonsoft.Json;
using NetTopologySuite.IO;
using NetTopologySuite.Geometries;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class DeploymentProfile : BaseProfile
    {
        public DeploymentProfile() : base()
        {
            CreateMap<DeploymentDTO, Deployment>()
                .ForMember(d => d.Key, opt => opt.Ignore())
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => GetTimeSpanFromTimestamp(s.StartTime)))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => GetTimeSpanFromTimestamp(s.EndTime)))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => ReadGeoJson<Point>(s.Location)))

                // Ignore references, we'll find them later
                .ForMember(d => d.InEastPortland, opt => opt.Ignore())
                .ForMember(d => d.Vehicle, opt => opt.Ignore())
                .ForMember(d => d.Company, opt => opt.Ignore())
                .ForMember(d => d.VehicleType, opt => opt.Ignore())
                .ForMember(d => d.StartDate, opt => opt.Ignore())
                .ForMember(d => d.EndDate, opt => opt.Ignore())
                .ForMember(d => d.PlacementReason, opt => opt.Ignore())
                .ForMember(d => d.PickupReason, opt => opt.Ignore());
        }
    }
}
