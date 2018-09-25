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
        private readonly IStreetSegmentRepository StreetSegmentRepository;
        private readonly IStreetSegmentGroupRepository StreetSegmentGroupRepository;
        private readonly HttpClient HttpClient;

        public TripService(
            ILogger<TripService> logger,
            APIOptions options,
            INeighborhoodRepository neighborhoodRepository,
            IPatternAreaRepository patternAreaRepository,
            IStreetSegmentRepository streetSegmentRepository,
            IStreetSegmentGroupRepository streetSegmentGroupRepository
        )
        {
            Logger = logger;

            NeighborhoodRepository = neighborhoodRepository;
            PatternAreaRepository = patternAreaRepository;
            StreetSegmentRepository = streetSegmentRepository;
            StreetSegmentGroupRepository = streetSegmentGroupRepository;

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
                var existingSegmentGroupTask = StreetSegmentGroupRepository.FindGroupKey(trip.AlternateKey);

                trip.NeighborhoodStartKey = (await neighborhoodStartTask)?.Key;
                trip.NeighborhoodEndKey = (await neighborhoodEndTask)?.Key;
                trip.PatternAreaStartKey = (await patternAreaStartTask)?.Key;
                trip.PatternAreaEndKey = (await patternAreaEndTask)?.Key;

                // if we've already calculated a segment group, don't repeat the expensive process
                trip.StreetSegmentGroupKey = await existingSegmentGroupTask ??
                    (await StreetSegmentGroupRepository.CreateGroup(
                        await StreetSegmentRepository.Find(Mapper.Map<LineString>(trip.Route))
                    ))?.Key;

                // add it to the database
                try
                {
                    var response = await HttpClient.PostAsJsonAsync("trip", trip);
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
