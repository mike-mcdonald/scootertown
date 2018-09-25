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
    public class StreetSegmentService : IStreetSegmentService
    {
        private readonly AreasOfInterest AreasOfInterest;
        private readonly IStreetSegmentRepository StreetSegmentRepository;

        public StreetSegmentService(
            IStreetSegmentRepository streetSegmentRepository,
            AreasOfInterest options
        )
        {
            StreetSegmentRepository = streetSegmentRepository;
            AreasOfInterest = options;
        }

        public async Task Save() =>
            await Save(GeoJsonReaders.ReadGeoJsonFile<StreetSegment>(AreasOfInterest.StreetSegmentsFile));

        public async Task Save(IEnumerable<StreetSegment> StreetSegments)
        {
            foreach (var streetSegment in StreetSegments)
            {
                await StreetSegmentRepository.Add(streetSegment, false);
            }
            await StreetSegmentRepository.SaveChanges();
        }
    }
}
