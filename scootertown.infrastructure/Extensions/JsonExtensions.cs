using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Infrastructure.JSON;

namespace PDX.PBOT.Scootertown.Infrastructure.Extensions
{
    public static class DeserializeExtensions
    {
        public static async Task<T> DeserializeJson<T>(this HttpResponseMessage response, T anonymousObject)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(stream));
            var serializer = JsonSerializer.Create();
            serializer.Converters.Add(new SafeGeoJsonConverter());
            return serializer.Deserialize<T>(reader);
        }

        public static T FromGeoJson<T>(this IGeometryObject geometry) where T : Geometry
        {
            if (geometry == null)
            {
                return default(T);
            }

            string json = JsonConvert.SerializeObject(geometry);
            GeoJsonReader reader = new GeoJsonReader();
            T result = reader.Read<T>(json);
            return result;
        }

        public static T ToGeoJson<T>(this Geometry geometry) where T : GeoJSONObject
        {
            if (geometry == null)
            {
                return default(T);
            }

            GeoJsonWriter writer = new GeoJsonWriter();
            var json = writer.Write(geometry);
            T result = JsonConvert.DeserializeObject<T>(json, new SafeGeoJsonConverter());
            return result;
        }
    }
}
