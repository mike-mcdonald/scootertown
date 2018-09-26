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
        private readonly IBicyclePathRepository BicyclePathRepository;
        private readonly IBicyclePathGroupRepository BicyclePathGroupRepository;
        private readonly HttpClient HttpClient;

        public TripService(
            ILogger<TripService> logger,
            APIOptions options,
            INeighborhoodRepository neighborhoodRepository,
            IPatternAreaRepository patternAreaRepository,
            IStreetSegmentRepository streetSegmentRepository,
            IStreetSegmentGroupRepository streetSegmentGroupRepository,
            IBicyclePathRepository bicyclePathRepository,
            IBicyclePathGroupRepository bicyclePathGroupRepository
        )
        {
            Logger = logger;

            NeighborhoodRepository = neighborhoodRepository;
            PatternAreaRepository = patternAreaRepository;
            StreetSegmentRepository = streetSegmentRepository;
            StreetSegmentGroupRepository = streetSegmentGroupRepository;
            BicyclePathRepository = bicyclePathRepository;
            BicyclePathGroupRepository = bicyclePathGroupRepository;

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

                trip.StreetSegmentGroupKey = await StreetSegmentGroupRepository.FindGroupKey(trip.AlternateKey);
                trip.BicyclePathGroupKey = await BicyclePathGroupRepository.FindGroupKey(trip.AlternateKey);

                // if we've already calculated a segment group, don't repeat the expensive process
                Task<List<StreetSegment>> segmentsTask = null;
                if (trip.StreetSegmentGroupKey == null)
                {
                    segmentsTask = StreetSegmentRepository.Find(Mapper.Map<LineString>(trip.Route));
                }
                Task<List<BicyclePath>> pathsTask = null;
                if (trip.BicyclePathGroupKey == null)
                {
                    pathsTask = BicyclePathRepository.Find(Mapper.Map<LineString>(trip.Route));
                }

                var neighborhoodStartTask = NeighborhoodRepository.Find(Mapper.Map<Point>(item.StartPoint));
                var neighborhoodEndTask = NeighborhoodRepository.Find(Mapper.Map<Point>(item.EndPoint));
                var patternAreaStartTask = PatternAreaRepository.Find(Mapper.Map<Point>(item.StartPoint));
                var patternAreaEndTask = PatternAreaRepository.Find(Mapper.Map<Point>(item.EndPoint));

                trip.NeighborhoodStartKey = (await neighborhoodStartTask)?.Key;
                trip.NeighborhoodEndKey = (await neighborhoodEndTask)?.Key;
                trip.PatternAreaStartKey = (await patternAreaStartTask)?.Key;
                trip.PatternAreaEndKey = (await patternAreaEndTask)?.Key;

                trip.StreetSegmentGroupKey = trip.StreetSegmentGroupKey ??
                    (await StreetSegmentGroupRepository.CreateGroup(await segmentsTask))?.Key;
                trip.BicyclePathGroupKey = trip.BicyclePathGroupKey ??
                    (await BicyclePathGroupRepository.CreateGroup(await pathsTask))?.Key;

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
