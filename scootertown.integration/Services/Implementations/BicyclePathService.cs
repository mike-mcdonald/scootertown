using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Infrastructure.JSON;
using PDX.PBOT.Scootertown.Integration.Options;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration.Services.Implementations
{
    public class BicyclePathService : IBicyclePathService
    {
        private readonly AreasOfInterest AreasOfInterest;
        private readonly IBicyclePathRepository BicyclePathRepository;

        public BicyclePathService(
            IBicyclePathRepository bicyclePathRepository,
            AreasOfInterest options
        )
        {
            BicyclePathRepository = bicyclePathRepository;
            AreasOfInterest = options;
        }

        public async Task Save() =>
            await Save(GeoJsonReaders.ReadGeoJsonFile<BicyclePath>(AreasOfInterest.BicyclePathsFile));

        public async Task Save(IEnumerable<BicyclePath> bicyclePaths)
        {
            foreach (var bicyclePath in bicyclePaths)
            {
                await BicyclePathRepository.Add(bicyclePath, false);
            }
            await BicyclePathRepository.SaveChanges();
        }
    }
}
