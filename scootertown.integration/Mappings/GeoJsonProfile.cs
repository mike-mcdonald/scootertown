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
using Newtonsoft.Json;
using NetTopologySuite.IO;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public class GeoJsonProfile : BaseProfile
    {
        public GeoJsonProfile() : base()
        {
            CreateMap<GeoJSON.Net.Feature.Feature, Neighborhood>()
                .ForMember(d => d.AlternateKey, opt => opt.MapFrom(s => s.Properties["OBJECTID"]))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Properties["MAPLABEL"]))
                .ForMember(d => d.Geometry, opt => opt.MapFrom(s => ReadGeoJson<Geometry>(s.Geometry)));

            CreateMap<GeoJSON.Net.Geometry.Polygon, Geometry>()
                .Substitute(s => ReadGeoJson<Geometry>(s));

            CreateMap<GeoJSON.Net.Geometry.MultiPolygon, Geometry>()
                .Substitute(s => ReadGeoJson<Geometry>(s));

            CreateMap<GeoJSON.Net.Geometry.IGeometryObject, Geometry>()
                .Substitute(s => ReadGeoJson<Geometry>(s));
        }
    }
}
