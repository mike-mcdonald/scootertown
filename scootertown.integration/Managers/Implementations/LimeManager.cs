using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers.Implementations
{
    public class LimeManager : CompanyManagerBase
    {
        public LimeManager(IConfigurationSection configuration) : base("Lime", configuration) { }

        public override async Task<Queue<DeploymentDTO>> RetrieveAvailability()
        {
            var page = 1;
            var total = new List<DeploymentDTO>();
            var current = new List<DeploymentDTO>();
            do
            {
                var response = await Client.GetAsync($"availability?page={page}");
                if (response.IsSuccessStatusCode)
                {
                    current = (await response.DeserializeJson(new { max_page = 1, data = new List<DeploymentDTO>() })).data;
                    total.AddRange(current);
                }
                else
                {
                    throw new Exception($"Error retrieving availability for {CompanyName}");
                }
            } while (current.Count > 0);

            return new Queue<DeploymentDTO>(total);
        }

        public override async Task<Queue<CollisionDTO>> RetrieveCollisions()
        {
            var response = await Client.GetAsync("collisions");
            if (response.IsSuccessStatusCode)
            {
                var collisions = (await response.DeserializeJson(new { data = new Queue<CollisionDTO>() })).data;
                return collisions;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<ComplaintDTO>> RetrieveComplaints()
        {
            var response = await Client.GetAsync("complaints");
            if (response.IsSuccessStatusCode)
            {
                var streamTask = await response.Content.ReadAsStringAsync();
                var complaints = (await response.DeserializeJson(new { max_page = 1, data = new Queue<ComplaintDTO>() })).data;
                return complaints;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<TripDTO>> RetrieveTrips(long offset = 0)
        {
            var trips = new Queue<TripDTO>();
            var page = Offset / 500 + 1;

            var response = await Client.GetAsync($"trips?page={page}");
            if (response.IsSuccessStatusCode)
            {
                trips = (await response.DeserializeJson(new { max_page = 1, data = new Queue<TripDTO>() })).data;
                Offset += trips.Count;
            }
            else
            {
                throw new Exception($"Error retrieving trips for {CompanyName}");
            }

            return new Queue<TripDTO>(trips.Select(t => Mapper.Map<TripDTO>(t)).ToList());
        }
    }
}
