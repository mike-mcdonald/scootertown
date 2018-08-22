using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Integration.Models;
using PDX.PBOT.Scootertown.Integration.Models.Lime;

namespace PDX.PBOT.Scootertown.Integration.Managers.Implementations
{
    public class LimeManager : CompanyManagerBase
    {
        public LimeManager(IConfigurationSection configuration) : base("Lime", configuration) { }

        public override async Task<List<Models.DeploymentDTO>> RetrieveAvailability()
        {
            var response = await Client.GetAsync("availability");
            if (response.IsSuccessStatusCode)
            {
                var streamTask = await response.Content.ReadAsStringAsync();
                var availability = JsonConvert.DeserializeAnonymousType(streamTask, new { max_page = 1, data = new List<Models.Lime.DeploymentDTO>() }).data;
                return availability.Select(a => Mapper.Map<Models.DeploymentDTO>(a)).ToList();
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<List<CollisionDTO>> RetrieveCollisions()
        {
            var response = await Client.GetAsync("collisions");
            if (response.IsSuccessStatusCode)
            {
                var streamTask = await response.Content.ReadAsStringAsync();
                var collisions = JsonConvert.DeserializeAnonymousType(streamTask, new { data = new List<CollisionDTO>() }).data;
                return collisions;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<List<ComplaintDTO>> RetrieveComplaints()
        {
            var response = await Client.GetAsync("complaints");
            if (response.IsSuccessStatusCode)
            {
                var streamTask = await response.Content.ReadAsStringAsync();
                var complaints = JsonConvert.DeserializeAnonymousType(streamTask, new { max_page = 1, data = new List<ComplaintDTO>() }).data;
                return complaints;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<List<Models.TripDTO>> RetrieveTrips(long offset = 0)
        {
            var trips = new List<Models.Lime.TripDTO>();
            var page = Offset / 500 + 1;

            var response = await Client.GetAsync($"trips?page={page}");
            if (response.IsSuccessStatusCode)
            {
                var streamTask = await response.Content.ReadAsStringAsync();
                trips = JsonConvert.DeserializeAnonymousType(streamTask, new { max_page = 1, data = new List<Models.Lime.TripDTO>() }).data;
                Offset += trips.Count;
            }
            else
            {
                throw new Exception($"Error retrieving trips for {CompanyName}");
            }

            return trips.Select(t => Mapper.Map<Models.TripDTO>(t)).ToList();
        }
    }
}
