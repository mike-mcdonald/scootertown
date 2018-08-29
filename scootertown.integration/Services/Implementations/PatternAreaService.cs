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
    public class PatternAreaService : IPatternAreaService
    {
        private readonly AreasOfInterest AreasOfInterest;
        private readonly IPatternAreaRepository PatternAreaRepository;

        public PatternAreaService(
            IPatternAreaRepository patternAreaRepository,
            IOptions<AreasOfInterest> optionsAccessor
        )
        {
            PatternAreaRepository = patternAreaRepository;
            AreasOfInterest = optionsAccessor.Value;
        }

        public async Task Save() =>
            await Save(GeoJsonReaders.ReadGeoJsonFile<Geometry>(AreasOfInterest.PatternAreasFile));

        public async Task Save(IEnumerable<PatternArea> patternAreas)
        {
            foreach (var patternArea in patternAreas)
            {
                await PatternAreaRepository.Add(patternArea, false);
            }
            await PatternAreaRepository.SaveChanges();
        }
    }
}
