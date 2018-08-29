using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Data.Models;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration.Services
{
    public abstract class ServiceBase<TSource> : IService<TSource>
       where TSource : ModelBase
    {
        protected readonly ICalendarRepository CalendarRepository;

        protected ServiceBase(ICalendarRepository calendarRepository)
        {
            CalendarRepository = calendarRepository;
        }

        public abstract Task Save(string company, Queue<TSource> items);

        protected async Task<Calendar> FindOrAddCalendar(long? timestamp)
        {
            if (!timestamp.HasValue)
            {
                return null;
            }

            System.DateTime date = (new DateTime()).FromUnixTimestamp(timestamp.Value);
            return await FindOrAddCalendar(date);
        }

        protected async Task<Calendar> FindOrAddCalendar(DateTime date)
        {
            var dbItem = await CalendarRepository.Find(date);

            if (dbItem == null)
            {
                dbItem = await CalendarRepository.Add(date);
            }

            return dbItem;
        }

        protected async Task<T> FindOrAdd<T>(IRepository<T> repository, string alternateKey, T item)
        {
            var dbItem = await repository.Find(alternateKey);

            if (dbItem == null)
            {
                dbItem = await repository.Add(item);
            }

            return dbItem;
        }

        protected async Task<T> FindOrAdd<T>(IRepository<T> repository, int key, T item)
        {
            var dbItem = await repository.Find(key);

            if (dbItem == null)
            {
                dbItem = await repository.Add(item);
            }

            return dbItem;
        }

        protected Queue<TSource> ReadGeoJsonFile<TGeometry>(string fileName)
            where TGeometry : IGeometryObject
        {
            var collection = JsonConvert.DeserializeObject<FeatureCollection>(
                File.ReadAllText(fileName)
            );

            var queue = new Queue<TSource>();
            foreach (var feature in collection.Features)
            {
                var item = Mapper.Map<TSource>(feature);
                queue.Enqueue(item);
            }
            return queue;
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
    }
}
