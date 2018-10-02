using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GeoJSON.Net.Geometry;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.JSON;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers.Implementations
{
    public class SkipManager : CompanyManagerBase
    {
        private string LastTrip { get; set; }

        public SkipManager(IConfigurationSection configuration) : base("Skip", configuration) { }

        public override async Task<Queue<DeploymentDTO>> RetrieveAvailability()
        {
            using (var response = await Client.GetAsync("availability.json"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var availability = await response.DeserializeJson(new List<DeploymentDTO>());
                    return new Queue<DeploymentDTO>(availability);
                }
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<CollisionDTO>> RetrieveCollisions()
        {
            using (var response = await Client.GetAsync("collisions.json"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var collisions = await response.DeserializeJson(new Queue<CollisionDTO>());
                    return collisions;
                }
            }

            throw new Exception($"Error retrieving collisions for {CompanyName}");
        }

        public override async Task<Queue<ComplaintDTO>> RetrieveComplaints()
        {
            using (var response = await Client.GetAsync("complaints.json"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var streamTask = await response.Content.ReadAsStringAsync();
                    var complaints = await response.DeserializeJson(new Queue<ComplaintDTO>());
                    return complaints;
                }
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<TripDTO>> RetrieveTrips()
        {
            var limit = 500;

            using (var response = await Client.GetAsync($"trips.json?limit={limit}&startAfter={LastTrip}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var trips = await response.DeserializeJson(new List<TripDTO>());
                        LastTrip = trips?.LastOrDefault()?.AlternateKey;
                        return new Queue<TripDTO>(trips);
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Error retrieving trips for {CompanyName}:\n{e.Message}");
                    }
                }
            }

            throw new Exception($"Error retrieving trips for {CompanyName}");
        }
    }
}
