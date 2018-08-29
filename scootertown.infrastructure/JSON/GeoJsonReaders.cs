using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Infrastructure.JSON;

namespace PDX.PBOT.Scootertown.Infrastructure.JSON
{
    public static class GeoJsonReaders
    {
        public static IEnumerable<T> ReadGeoJsonFile<T>(string fileName)
        {
            var collection = JsonConvert.DeserializeObject<FeatureCollection>(
                File.ReadAllText(fileName)
            );

            var queue = new List<T>();
            foreach (var feature in collection.Features)
            {
                var item = Mapper.Map<T>(feature);
                queue.Add(item);
            }
            return queue;
        }
    }
}
