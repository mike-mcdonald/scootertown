using System;
using AutoMapper;
using GeoJSON.Net;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Integration.Infrastructure;

namespace PDX.PBOT.Scootertown.Integration.Mappings
{
    public abstract class BaseProfile : Profile
    {
        protected TimeSpan? GetTimeSpanFromTimestamp(long? timestamp)
        {
            if (!timestamp.HasValue)
            {
                return null;
            }
            var datetime = new DateTime().FromUnixTimestamp(timestamp.Value);
            return datetime.TimeOfDay;
        }

        protected T ReadGeoJson<T>(GeoJSONObject geoJSON) where T : Geometry
        {
            if (geoJSON == null)
            {
                return null;
            }

            string json = JsonConvert.SerializeObject(geoJSON);
            GeoJsonReader reader = new GeoJsonReader();
            T result = reader.Read<T>(json);
            return result;
        }
    }
}
