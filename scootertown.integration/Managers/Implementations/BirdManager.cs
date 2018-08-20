using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Integration.Infrastructure;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers.Implementations
{
    public class BirdManager : CompanyManagerBase
    {
        public BirdManager(IConfigurationSection configuration) : base("Bird", configuration) { }

        public override async Task<List<DeploymentDTO>> RetrieveAvailability()
        {
            var limit = 500;
            var offset = 0;
            var retrievedAvailability = new List<DeploymentDTO>();
            var totalAvailability = new List<DeploymentDTO>();

            do
            {
                var response = await Client.GetAsync($"availability?limit={limit}&offset={offset}");
                if (response.IsSuccessStatusCode)
                {
                    var streamTask = response.Content.ReadAsStringAsync();
                    retrievedAvailability = JsonConvert.DeserializeAnonymousType(await streamTask, new { availability = new List<DeploymentDTO>() }).availability;
                    totalAvailability.AddRange(retrievedAvailability);
                    offset += retrievedAvailability.Count;
                }
                else
                {
                    throw new Exception($"Error retrieving availability for {CompanyName}");
                }
            } while (retrievedAvailability.Count != 0);

            return totalAvailability;
        }

        public override async Task<List<CollisionDTO>> RetrieveCollisions()
        {
            var response = await Client.GetAsync("collisions");
            if (response.IsSuccessStatusCode)
            {
                var streamTask = response.Content.ReadAsStringAsync();
                var collisions = JsonConvert.DeserializeAnonymousType(await streamTask, new { collisions = new List<CollisionDTO>() }).collisions;
                return collisions;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<List<ComplaintDTO>> RetrieveComplaints()
        {
            var response = await Client.GetAsync("complaints");
            if (response.IsSuccessStatusCode)
            {
                var streamTask = response.Content.ReadAsStringAsync();
                var complaints = JsonConvert.DeserializeAnonymousType(await streamTask, new { complaints = new List<ComplaintDTO>() }).complaints;
                return complaints;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<List<TripDTO>> RetrieveTrips(long offset = 0)
        {
            var limit = 500;
            var totalTrips = new List<TripDTO>();
            var retrievedTrips = new List<TripDTO>();

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new SafeGeoJsonConverter());

            var response = await Client.GetAsync($"trips?limit={limit}&offset={Offset}");
            if (response.IsSuccessStatusCode)
            {
                var streamTask = await response.Content.ReadAsStringAsync();
                retrievedTrips = JsonConvert.DeserializeAnonymousType(streamTask, new { trips = new List<TripDTO>() }, settings).trips;
                totalTrips.AddRange(retrievedTrips);
                Offset += retrievedTrips.Count;
            }
            else
            {
                throw new Exception($"Error retrieving trips for {CompanyName}");
            }

            return totalTrips;
        }
    }
}
