using System;
using AutoMapper;
using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.JSON;

namespace PDX.PBOT.Scootertown.Infrastructure.Mappings
{
    public abstract class BaseProfile : Profile
    {
        protected DateTime? GetDateTimeFromTimestamp(long? timestamp)
        {
            if (!timestamp.HasValue)
            {
                return null;
            }
            var datetime = new DateTime().FromUnixTimestamp(timestamp.Value).ToLocalTime();
            return datetime;
        }

        protected T ReadGeoJson<T>(IGeometryObject geometry) where T : Geometry
        {
            if (geometry == null)
            {
                return null;
            }

            string json = JsonConvert.SerializeObject(geometry);
            GeoJsonReader reader = new GeoJsonReader();
            T result = reader.Read<T>(json);
            return result;
        }

        protected T WriteGeoJson<T>(Geometry geometry) where T : GeoJSONObject
        {
            if (geometry == null)
            {
                return null;
            }

            GeoJsonWriter writer = new GeoJsonWriter();
            var json = writer.Write(geometry);
            T result = JsonConvert.DeserializeObject<T>(json, new SafeGeoJsonConverter());
            return result;
        }
    }
}
