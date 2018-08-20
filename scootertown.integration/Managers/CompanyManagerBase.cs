using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using GeoJSON.Net.Geometry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PDX.PBOT.Scootertown.Integration.Managers.Interfaces;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers
{
    public abstract class CompanyManagerBase : ICompanyManager
    {
        protected readonly string CompanyName;
        protected long Offset;
        protected readonly HttpClient Client;

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
        }

        public abstract Task<List<DeploymentDTO>> RetrieveAvailability();
        public abstract Task<List<CollisionDTO>> RetrieveCollisions();
        public abstract Task<List<ComplaintDTO>> RetrieveComplaints();
        public abstract Task<List<TripDTO>> RetrieveTrips(long offset);
    }
}