using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Integration.Models;
using PDX.PBOT.Scootertown.Integration.Options;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration.Services.Implementations
{
    public class CollisionService : ICollisionService
    {
        private readonly ILogger Logger;
        private readonly INeighborhoodRepository NeighborhoodRepository;
        private readonly IPatternAreaRepository PatternAreaRepository;
        private readonly HttpClient HttpClient;

        public CollisionService(
            ILogger<CollisionService> logger,
            APIOptions options,
            INeighborhoodRepository neighborhoodRepository,
            IPatternAreaRepository patternAreaRepository
        )
        {
            Logger = logger;
            NeighborhoodRepository = neighborhoodRepository;
            PatternAreaRepository = patternAreaRepository;

            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(options.BaseAddress);
        }
        public async Task Save(string company, Queue<CollisionDTO> items)
        {
            CollisionDTO item;
            while (items.TryDequeue(out item))
            {

                var collision = Mapper.Map<API.Models.CollisionDTO>(item);

                var neighborhoodTask = NeighborhoodRepository.Find(Mapper.Map<Point>(item.Location));
                var patternAreaTask = NeighborhoodRepository.Find(Mapper.Map<Point>(item.Location));

                collision.NeighborhoodKey = (await neighborhoodTask)?.Key;
                collision.PatternAreaKey = (await patternAreaTask)?.Key;

                // add it to the database
                try
                {
                    var response = await HttpClient.PostAsJsonAsync("collision", collision);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException e)
                {
                    Logger.LogWarning("Error saving trip record for {company}:\n{message}", company, e.Message);
                }
            }
        }
    }
}
