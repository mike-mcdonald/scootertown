using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Integration.Models;
using PDX.PBOT.Scootertown.Integration.Options;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration.Services.Implementations
{
    public class TripService : ITripService
    {
        private readonly ILogger Logger;
        private readonly INeighborhoodRepository NeighborhoodRepository;
        private readonly IPatternAreaRepository PatternAreaRepository;
        private readonly HttpClient HttpClient;

        public TripService(
            ILogger<TripService> logger,
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

        public async Task<long> GetTotalTrips(string companyName)
        {
            var response = await HttpClient.GetStringAsync($"trip/{companyName}");
            return Convert.ToInt64(response);
        }

        public async Task Save(string company, Queue<TripDTO> items)
        {
            while (items.Count > 0)
            {
                var item = items.Dequeue();

                var trip = Mapper.Map<API.Models.TripDTO>(item);

                var neighborhoodStartTask = NeighborhoodRepository.Find(Mapper.Map<Point>(item.StartPoint));
                var neighborhoodEndTask = NeighborhoodRepository.Find(Mapper.Map<Point>(item.EndPoint));
                var patternAreaStartTask = PatternAreaRepository.Find(Mapper.Map<Point>(item.StartPoint));
                var patternAreaEndTask = PatternAreaRepository.Find(Mapper.Map<Point>(item.EndPoint));

                trip.NeighborhoodStart = (await neighborhoodStartTask)?.Key;
                trip.NeighborhoodEnd = (await neighborhoodEndTask)?.Key;
                trip.PatternAreaStartKey = (await patternAreaStartTask)?.Key;
                trip.PatternAreaEndKey = (await patternAreaEndTask)?.Key;

                // add it to the database
                try
                {
                    var response = await HttpClient.PostAsJsonAsync("trip", trip);
                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    Logger.LogWarning("Error saving trip record for {company}", company);
                }
            }
        }
    }
}
