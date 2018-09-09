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
    public class ComplaintService : IComplaintService
    {
        private readonly ILogger Logger;
        private readonly INeighborhoodRepository NeighborhoodRepository;
        private readonly IPatternAreaRepository PatternAreaRepository;
        private readonly HttpClient HttpClient;

        public ComplaintService(
            ILogger<ComplaintService> logger,
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
        public async Task Save(string company, Queue<ComplaintDTO> items)
        {
            ComplaintDTO item;
            while (items.TryDequeue(out item))
            {

                var complaint = Mapper.Map<API.Models.ComplaintDTO>(item);

                // add it to the database
                try
                {
                    var response = await HttpClient.PostAsJsonAsync("complaint", complaint);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException e)
                {
                    Logger.LogWarning("Error saving complaint record for {company}:\n{message}", company, e.Message);
                }
            }
        }
    }
}
