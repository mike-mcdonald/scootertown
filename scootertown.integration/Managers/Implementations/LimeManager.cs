using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
            var page = 0;
            var total = new List<DeploymentDTO>();
            var current = new List<DeploymentDTO>();
            do
            {
                using (var response = await Client.GetAsync($"availability?page={page}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        current = (await response.DeserializeJson(new { max_page = 1, data = new List<DeploymentDTO>() })).data;
                        total.AddRange(current);
                    }
                    else
                    {
                        throw new Exception($"Error retrieving availability for {CompanyName}");
                    }
                }
                page += 1;
            } while (current.Count > 0);

            return new Queue<DeploymentDTO>(total);
        }

        public override async Task<Queue<CollisionDTO>> RetrieveCollisions()
        {
            using (var response = await Client.GetAsync("collisions"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var collisions = (await response.DeserializeJson(new { data = new Queue<CollisionDTO>() })).data;
                    return collisions;
                }
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<ComplaintDTO>> RetrieveComplaints()
        {
            using (var response = await Client.GetAsync("complaints"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var streamTask = await response.Content.ReadAsStringAsync();
                    var complaints = (await response.DeserializeJson(new { max_page = 1, data = new Queue<ComplaintDTO>() })).data;
                    return complaints;
                }
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<TripDTO>> RetrieveTrips()
        {
            var trips = new Queue<TripDTO>();

            using (var response = await Client.GetAsync($"trips?page={Offset}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content.Headers.ContentType.MediaType == "text/html")
                    {
                        // Lime returns an html page for their 500 error that indicates they ran out of trips to return
                        //  return the empty queue.
                        return trips;
                    }
                    trips = (await response.DeserializeJson(new { max_page = 1, data = new Queue<TripDTO>() })).data;
                    Offset += 1;

                    return new Queue<TripDTO>(trips.Select(t => Mapper.Map<TripDTO>(t)).ToList());
                }
            }

            throw new Exception($"Error retrieving trips for {CompanyName}");
        }
    }
}
