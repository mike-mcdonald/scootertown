using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeoJSON.Net.Feature;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;

namespace PDX.PBOT.Scootertown.API.Controllers
{
    [Route("api/[controller]")]
    public class TripController : Controller
    {
        private readonly ILogger Logger;
        private readonly ITripRepository TripRepository;
        private readonly ICalendarRepository CalendarRepository;
        private readonly ICompanyRepository CompanyRepository;
        private readonly IVehicleRepository VehicleRepository;

        public TripController(
            ILogger<TripController> logger,
            ITripRepository tripRepository,
            ICalendarRepository calendarRepository,
            ICompanyRepository companyRepository,
            IVehicleRepository vehicleRepository
        )
        {
            Logger = logger;
            TripRepository = tripRepository;
            CalendarRepository = calendarRepository;
            CompanyRepository = companyRepository;
            VehicleRepository = vehicleRepository;
        }

        // GET api/trip
        [HttpGet]
        public async Task<long> GetAsync() =>
            await TripRepository.Count();


        // GET api/trip/5
        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync(long key)
        {
            var deployment = await TripRepository.Find(key);
            return Ok(Mapper.Map<TripDTO>(deployment));
        }

        // GET api/trip/lime
        [HttpGet("{company}")]
        public async Task<long> GetAsync(string company) =>
            await TripRepository.CountByCompany(company);

        [HttpGet("as/geojson")]
        public async Task<IActionResult> GetAsGeojsonAsync()
        {
            var tripsTask = TripRepository.All();

            var featureCollection = new FeatureCollection();

            try
            {
                var trips = await tripsTask;

                featureCollection.Features.AddRange(trips.Select(t => Mapper.Map<Feature>(t)));

                return Ok(featureCollection);
            }
            catch (Exception e)
            {
                Logger.LogError("Error retrieving trips: {message}", e.Message);
                return BadRequest(e.ToString());
            }
        }

        [HttpGet("by/neighborhood")]
        public async Task<IActionResult> ByNeighborhoodAsync()
        {
            var result = await TripRepository.Queryable()
                .GroupBy(
                    x => new
                    {
                        NeighborhoodStartName = x.NeighborhoodStart.Name,
                        NeighborhoodEndName = x.NeighborhoodEnd.Name
                    },
                    x => x,
                    (key, g) => new
                    {
                        NeighborhoodStartName = key.NeighborhoodStartName,
                        NeighborhoodEndName = key.NeighborhoodEndName,
                        Trips = g.Select(x => x.Route.ToGeoJson<GeoJSON.Net.Geometry.LineString>()).ToList()
                    }
                )
                .ToAsyncEnumerable()
                .ToList();

            return Ok(result);

        }

        // POST api/trip
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]TripDTO value)
        {
            var now = DateTime.Now;

            var existingTask = TripRepository.Find(value.AlternateKey);

            var trip = Mapper.Map<Trip>(value);

            trip.FirstSeen = trip.LastSeen = now;

            // Get the reference properties set up
            var vehicleTask = VehicleRepository.Find(value.VehicleName);
            var endDateTask = CalendarRepository.Find(value.EndTime);
            var companyTask = CompanyRepository.Find(value.CompanyName);

            // await this first before finding the next date
            trip.EndDateKey = (await endDateTask).Key;
            var startDateTask = CalendarRepository.Find(value.StartTime);

            trip.CompanyKey = (await companyTask).Key;

            trip.StartDateKey = (await startDateTask).Key;

            var vehicle = await vehicleTask ?? await VehicleRepository.Add(new Vehicle { Name = value.VehicleName });
            trip.VehicleKey = vehicle.Key;

            var existing = await existingTask;

            if (existing != null)
            {
                trip.Key = existing.Key;
                trip.FirstSeen = existing.FirstSeen == DateTime.MinValue ? now : existing.FirstSeen;
            }

            var upsertTask = existing == null ? TripRepository.Add(trip, false) : TripRepository.Update(trip, false);

            try
            {
                await upsertTask;
                await TripRepository.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.LogError("Error adding trip:\n{message}\n{inner}", e.Message, e.InnerException?.Message);
                return BadRequest(e.ToString());
            }
            return Ok(Mapper.Map<TripDTO>(trip));
        }

        // PUT api/trip/5
        [HttpPut("{key}")]
        public void Update(long key, [FromBody]TripDTO value)
        {
        }
    }
}
