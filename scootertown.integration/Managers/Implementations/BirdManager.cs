using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers.Implementations
{
    public class BirdManager : CompanyManagerBase
    {
        public BirdManager(IConfigurationSection configuration) : base("Bird", configuration) { }

        public override async Task<Queue<DeploymentDTO>> RetrieveAvailability()
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
                    retrievedAvailability = (await DeserializeJson(response, new { availability = new List<DeploymentDTO>() })).availability;
                    totalAvailability.AddRange(retrievedAvailability);
                    offset += retrievedAvailability.Count;
                }
                else
                {
                    throw new Exception($"Error retrieving availability for {CompanyName}");
                }
            } while (retrievedAvailability.Count != 0);

            return new Queue<DeploymentDTO>(totalAvailability);
        }

        public override async Task<Queue<CollisionDTO>> RetrieveCollisions()
        {
            var response = await Client.GetAsync("collisions");
            if (response.IsSuccessStatusCode)
            {
                var collisions = (await DeserializeJson(response, new { collisions = new Queue<CollisionDTO>() })).collisions;
                return collisions;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<ComplaintDTO>> RetrieveComplaints()
        {
            var response = await Client.GetAsync("complaints");
            if (response.IsSuccessStatusCode)
            {
                var complaints = (await DeserializeJson(response, new { complaints = new Queue<ComplaintDTO>() })).complaints;
                return complaints;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<TripDTO>> RetrieveTrips(long offset = 0)
        {
            var limit = 500;
            var trips = new Queue<TripDTO>();

            var response = await Client.GetAsync($"trips?limit={limit}&offset={Offset}");
            if (response.IsSuccessStatusCode)
            {
                trips = (await response.DeserializeJson(new { trips = new Queue<TripDTO>() })).trips;
                Offset += trips.Count;
            }
            else
            {
                throw new Exception($"Error retrieving trips for {CompanyName}");
            }

            return trips;
        }
    }
}
