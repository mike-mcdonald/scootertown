using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.JSON;
using PDX.PBOT.Scootertown.Integration.Options;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration.Services.Implementations
{
    public class DeploymentService : IDeploymentService
    {
        private readonly ILogger Logger;
        private readonly INeighborhoodRepository NeighborhoodRepository;
        private readonly HttpClient HttpClient;

        private readonly Polygon EastPortland;

        public DeploymentService(
            ILogger<DeploymentService> logger,
            IOptions<APIOptions> options,
            INeighborhoodRepository neighborhoodRepository
        )
        {
            Logger = logger;
            NeighborhoodRepository = neighborhoodRepository;

            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(options.Value.BaseAddress);
        }

        public async Task Save(string company, Queue<Integration.Models.DeploymentDTO> items)
        {
            var now = DateTime.Now;

            // get all the active deployments
            //  this is the only awaited one so I can manage the list of active deployments
            var response = await HttpClient.GetAsync($"deployment/{company}/active");
            var activeDeployments = await response.DeserializeJson(new List<API.Models.DeploymentDTO>());

            Logger.LogDebug($"Found {activeDeployments.Count} active deployments for {company}.");

            Models.DeploymentDTO item;
            var currentDeployments = 0;
            var newDeployments = 0;
            while (items.TryDequeue(out item))
            {
                try
                {
                    // Use automapper for the original properties
                    var deployment = Mapper.Map<API.Models.DeploymentDTO>(item);

                    // for this vehicle, is there an active deployment?
                    var currentDeployment = activeDeployments.FirstOrDefault(x => x.Vehicle == deployment.Vehicle);
                    if (currentDeployment != null)
                    {
                        currentDeployments += 1;
                        // if there is, update LastSeen
                        currentDeployment.LastSeen = now;
                        // only other thing that we should track as changed is a possible end time
                        currentDeployment.EndTime = item.EndTime.ToDateTime();
                        await HttpClient.PutAsJsonAsync<API.Models.DeploymentDTO>($"deployment/{currentDeployment.Key}", currentDeployment);
                        // remove from active list
                        activeDeployments.Remove(currentDeployment);
                    }
                    else
                    {
                        // if there isn't start a new one
                        newDeployments += 1;

                        // Get the reference properties set up
                        var neighborhoodTask = NeighborhoodRepository.Find(Mapper.Map<Point>(deployment.Location));

                        deployment.Neighborhood = (await neighborhoodTask)?.Key;

                        deployment.FirstSeen = deployment.LastSeen = now;
                        await HttpClient.PostAsJsonAsync("deployment", deployment);
                    }
                }
                catch (Exception e)
                {
                    // this might be because we are seeing something for the first time
                    Logger.LogWarning("Failed to save a deployment record:\n{message}\n{inner}", e.Message, e.InnerException.Message);
                }
            }

            Logger.LogDebug($"Found {currentDeployments} existing deployments and {newDeployments} new deployments.");

            // for all the other active deployments that we didn't see, update the end time to now
            foreach (var deploymentToEnd in activeDeployments)
            {
                try
                {
                    await HttpClient.PostAsync($"deployment/{deploymentToEnd.Key}/end", null);
                }
                catch (Exception e)
                {
                    // this might be because we are seeing something for the first time
                    Logger.LogWarning("Failed to save a deployment record:\n{message}\n{inner}", e.Message, e.InnerException.Message);
                }
            }
        }
    }
}
