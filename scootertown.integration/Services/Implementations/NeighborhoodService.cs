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
using PDX.PBOT.Scootertown.Integration.Options;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration.Services.Implementations
{
    public class NeighborhoodService : ServiceBase<Neighborhood, Neighborhood>, INeighborhoodService
    {
        private readonly AreasOfInterest AreasOfInterest;
        private readonly INeighborhoodRepository NeighborhoodRepository;

        public NeighborhoodService(
            ICalendarRepository calendarRepository,
            INeighborhoodRepository neighborhoodRepository,
            IOptions<AreasOfInterest> optionsAccessor
        ) : base(calendarRepository)
        {
            NeighborhoodRepository = neighborhoodRepository;
            AreasOfInterest = optionsAccessor.Value;
        }

        public async Task Save() =>
            await Save(ReadGeoJsonFile<Polygon>(AreasOfInterest.NeighborhoodsFile));

        public override async Task Save(List<Neighborhood> neighborhoods)
        {
            foreach (var neighborhood in neighborhoods)
            {
                await NeighborhoodRepository.Add(neighborhood, false);
            }
            await NeighborhoodRepository.SaveChanges();
        }
    }
}
