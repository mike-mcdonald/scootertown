using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.API.Controllers
{
    [Route("api/[controller]")]
    public class TripController : Controller
    {
        private readonly ILogger Logger;
        private readonly ITripRepository TripRepository;
        private readonly ICalendarRepository CalendarRepository;
        private readonly ICompanyRepository CompanyRepository;
        private readonly INeighborhoodRepository NeighborhoodRepository;
        private readonly IPaymentTypeRepository PaymentTypeRepository;
        private readonly IVehicleRepository VehicleRepository;
        private readonly IVehicleTypeRepository VehicleTypeRepository;

        public TripController(
            ILogger<TripController> logger,
            ITripRepository tripRepository,
            ICalendarRepository calendarRepository,
            ICompanyRepository companyRepository,
            INeighborhoodRepository neighborhoodRepository,
            IPaymentTypeRepository paymentTypeRepository,
            IVehicleRepository vehicleRepository,
            IVehicleTypeRepository vehicleTypeRepository
        )
        {
            Logger = logger;
            TripRepository = tripRepository;
            CalendarRepository = calendarRepository;
            CompanyRepository = companyRepository;
            NeighborhoodRepository = neighborhoodRepository;
            PaymentTypeRepository = paymentTypeRepository;
            VehicleRepository = vehicleRepository;
            VehicleTypeRepository = vehicleTypeRepository;
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

        // POST api/trip
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]TripDTO value)
        {
            var now = DateTime.Now;

            var existingTask = TripRepository.Find(value.AlternateKey);

            var trip = Mapper.Map<Trip>(value);

            trip.FirstSeen = trip.LastSeen = now;

            var vehicle = await VehicleRepository.Find(value.Vehicle) ?? await VehicleRepository.Add(new Vehicle { Name = value.Vehicle });
            var endDateTask = CalendarRepository.Find(value.EndTime);

            trip.VehicleKey = vehicle.Key;

            // if there isn't start a new one

            // await this first before finding the next date
            trip.EndDateKey = (await endDateTask).Key;

            // Get the reference properties set up
            var companyTask = CompanyRepository.Find(value.Company);
            var startDateTask = CalendarRepository.Find(value.StartTime);
            var vehicleTypeTask = VehicleTypeRepository.Find(value.VehicleType);
            var paymentTypeTask = PaymentTypeRepository.Find(value.PaymentType);
            var paymentAccessTask = PaymentTypeRepository.Find(value.PaymentAccess);

            trip.CompanyKey = (await companyTask).Key;
            trip.StartDateKey = (await startDateTask).Key;
            trip.VehicleTypeKey = (await vehicleTypeTask).Key;
            trip.PaymentTypeKey = (await paymentTypeTask).Key;
            trip.PaymentAccessKey = (await paymentAccessTask).Key;

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
                Logger.LogError("Error adding deployment:\n{message}\n{inner}", e.Message, e.InnerException?.Message);
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
