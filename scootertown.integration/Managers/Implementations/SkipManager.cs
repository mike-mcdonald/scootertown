using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GeoJSON.Net.Geometry;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Integration.Infrastructure;
using PDX.PBOT.Scootertown.Integration.Models;

namespace PDX.PBOT.Scootertown.Integration.Managers.Implementations
{
    public class SkipManager : CompanyManagerBase
    {
        public SkipManager(IConfigurationSection configuration) : base("Skip", configuration) { }

        public override async Task<Queue<DeploymentDTO>> RetrieveAvailability()
        {
            var response = await Client.GetAsync("availability.json");
            if (response.IsSuccessStatusCode)
            {
                var streamTask = await response.Content.ReadAsStringAsync();
                var availability = JsonConvert.DeserializeObject<List<DeploymentDTO>>(streamTask, JsonSettings);
                availability.ForEach(deployment =>
                {
                    // flip the coordinates
                    deployment.Location = new Point(new Position(
                        deployment.Location.Coordinates.Longitude,
                        deployment.Location.Coordinates.Latitude
                    ));
                });
                return new Queue<DeploymentDTO>(availability);
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<CollisionDTO>> RetrieveCollisions()
        {
            var builder = new UriBuilder("collisions.json");
            string url = builder.ToString();

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var streamTask = await response.Content.ReadAsStringAsync();
                var collisions = JsonConvert.DeserializeObject<Queue<CollisionDTO>>(streamTask, JsonSettings);
                return collisions;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<ComplaintDTO>> RetrieveComplaints()
        {
            var response = await Client.GetAsync("complaints.json");
            if (response.IsSuccessStatusCode)
            {
                var streamTask = await response.Content.ReadAsStringAsync();
                var complaints = JsonConvert.DeserializeObject<Queue<ComplaintDTO>>(streamTask, JsonSettings);
                return complaints;
            }

            throw new Exception($"Error retrieving availability for {CompanyName}");
        }

        public override async Task<Queue<TripDTO>> RetrieveTrips(long offset = 0)
        {
            var response = await Client.GetAsync($"trips.json");
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var reader = new JsonTextReader(new StreamReader(stream));
                var serializer = new JsonSerializer();
                serializer.Converters.Add(new SafeGeoJsonConverter());
                try
                {
                    var trips = serializer.Deserialize<List<TripDTO>>(reader);
                    trips.ForEach(trip =>
                    {
                        // flip the coordinates
                        trip.StartPoint = new Point(new Position(
                                trip.StartPoint.Coordinates.Longitude,
                                trip.StartPoint.Coordinates.Latitude
                            ));

                        trip.EndPoint = new Point(new Position(
                            trip.StartPoint.Coordinates.Longitude,
                            trip.StartPoint.Coordinates.Latitude
                        ));

                        var route = new List<IPosition>();
                        trip.Route.Coordinates.ToAsyncEnumerable().ForEach(position =>
                        {
                            route.Add(new Position(
                                position.Longitude,
                                position.Latitude
                            ));
                        });
                        trip.Route = new LineString(route);
                    });

                    return new Queue<TripDTO>(trips);
                }
                catch (Exception e)
                {
                    throw new Exception($"Error retrieving trips for {CompanyName}:\n{e.Message}");
                }
            }

            throw new Exception($"Error retrieving trips for {CompanyName}");
        }
    }
}
