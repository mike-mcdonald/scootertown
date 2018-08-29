using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Infrastructure.JSON;
using PDX.PBOT.Scootertown.Integration.Managers.Interfaces;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers
{
    public abstract class CompanyManagerBase : ICompanyManager
    {
        protected readonly string CompanyName;
        protected long Offset;
        protected readonly HttpClient Client;
        protected readonly JsonSerializerSettings JsonSettings;

        public string Company
        {
            get => CompanyName;
        }

        public long StartingOffset
        {
            set => Offset = value;
        }

        public CompanyManagerBase(string name, IConfigurationSection configuration)
        {
            CompanyName = name;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(configuration["BaseUrl"]);

            // we don't want cached availability especially
            var cacheControl = new CacheControlHeaderValue();
            cacheControl.NoCache = true;
            Client.DefaultRequestHeaders.CacheControl = cacheControl;

            foreach (var setting in configuration.GetSection("Headers").GetChildren())
            {
                Client.DefaultRequestHeaders.Add(setting.Key, setting.Value);
            }

            JsonSettings = new JsonSerializerSettings();
            JsonSettings.Converters.Add(new SafeGeoJsonConverter());
        }

        public abstract Task<Queue<DeploymentDTO>> RetrieveAvailability();
        public abstract Task<Queue<CollisionDTO>> RetrieveCollisions();
        public abstract Task<Queue<ComplaintDTO>> RetrieveComplaints();
        public abstract Task<Queue<TripDTO>> RetrieveTrips(long offset);

        protected async Task<T> DeserializeJson<T>(HttpResponseMessage response, T anonymousObject)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(stream));
            var serializer = JsonSerializer.Create();
            serializer.Converters.Add(new SafeGeoJsonConverter());
            return serializer.Deserialize<T>(reader);
        }
    }
}
