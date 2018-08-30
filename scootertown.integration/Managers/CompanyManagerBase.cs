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
        protected readonly HttpClient Client;
        protected readonly JsonSerializerSettings JsonSettings;

        public string Company
        {
            get => CompanyName;
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

        public abstract Task<Queue<DeploymentDTO>> RetrieveAvailability();
        public abstract Task<Queue<CollisionDTO>> RetrieveCollisions();
        public abstract Task<Queue<ComplaintDTO>> RetrieveComplaints();
        public abstract Task<Queue<TripDTO>> RetrieveTrips(long offset);
    }
}
