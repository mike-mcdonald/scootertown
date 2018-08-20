using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers.Implementations
{
    public class LimeManager : CompanyManagerBase
    {
        public LimeManager(IConfigurationSection configuration) : base("Lime", configuration) { }

        public override async Task<List<DeploymentDTO>> RetrieveAvailability()
        {
            var builder = new UriBuilder("availability");
            string url = builder.ToString();

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var streamTask = response.Content.ReadAsStringAsync();
                var availability = JsonConvert.DeserializeAnonymousType(await streamTask, new { data = new List<DeploymentDTO>() }).data;
                return availability;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<List<CollisionDTO>> RetrieveCollisions()
        {
            var builder = new UriBuilder("collisions");
            string url = builder.ToString();

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var streamTask = response.Content.ReadAsStringAsync();
                var collisions = JsonConvert.DeserializeAnonymousType(await streamTask, new { data = new List<CollisionDTO>() }).data;
                return collisions;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<List<ComplaintDTO>> RetrieveComplaints()
        {
            var builder = new UriBuilder("complaints");
            string url = builder.ToString();

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var streamTask = response.Content.ReadAsStringAsync();
                var complaints = JsonConvert.DeserializeAnonymousType(await streamTask, new { data = new List<ComplaintDTO>() }).data;
                return complaints;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<List<TripDTO>> RetrieveTrips(long offset = 0)
        {
            var totalTrips = new List<TripDTO>();
            var retrievedTrips = new List<TripDTO>();

            do
            {
                var response = await Client.GetAsync($"trips?page={Offset}");
                if (response.IsSuccessStatusCode)
                {
                    var streamTask = response.Content.ReadAsStringAsync();
                    retrievedTrips = JsonConvert.DeserializeAnonymousType(await streamTask, new { data = new List<TripDTO>() }).data;
                    totalTrips.AddRange(retrievedTrips);
                    Offset += 1;
                }
            } while (retrievedTrips.Count != 0);

            return totalTrips;
        }
    }
}
