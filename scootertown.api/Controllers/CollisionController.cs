using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.API.Controllers
{
    [Route("api/[controller]")]
    public class CollisionController : Controller
    {
        private readonly ILogger Logger;
        private readonly ICollisionRepository CollisionRepository;
        private readonly ICalendarRepository CalendarRepository;
        private readonly ICompanyRepository CompanyRepository;
        private readonly IVehicleRepository VehicleRepository;
        private readonly IVehicleTypeRepository VehicleTypeRepository;
        private readonly IStatusRepository StatusRepository;
        private readonly ITripRepository TripRepository;

        public CollisionController(
            ILogger<CollisionController> logger,
            ICollisionRepository collisionRepository,
            ICalendarRepository calendarRepository,
            ICompanyRepository companyRepository,
            IVehicleRepository vehicleRepository,
            IVehicleTypeRepository vehicleTypeRepository,
            IStatusRepository statusRepository,
            ITripRepository tripRepository
        )
        {
            Logger = logger;

            CollisionRepository = collisionRepository;
            CalendarRepository = calendarRepository;
            CompanyRepository = companyRepository;
            VehicleRepository = vehicleRepository;
            VehicleTypeRepository = vehicleTypeRepository;
            StatusRepository = statusRepository;
            TripRepository = tripRepository;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CollisionDTO value)
        {
            var now = DateTime.Now;

            try
            {
                var collision = await FillCollision(value);

                collision.LastSeen = now;

                var existing = await CollisionRepository.Find(value.Date, collision.Location);
                if (existing != null)
                {
                    collision.Key = existing.Key;
                    collision.FirstSeen = existing.FirstSeen == DateTime.MinValue ? now : existing.FirstSeen;
                    await CollisionRepository.Update(collision);
                }
                else
                {
                    collision.FirstSeen = now;
                    await CollisionRepository.Add(collision);
                }

                return Ok(Mapper.Map<CollisionDTO>(collision));
            }
            catch (Exception e)
            {
                Logger.LogError("Error adding collision:\n{message}", e.Message);
                return BadRequest(e.ToString());
            }
        }

        private async Task<Collision> FillCollision(CollisionDTO value)
        {
            var collision = Mapper.Map<Collision>(value);

            var calendarTask = CalendarRepository.Find(value.Date);
            var companyTask = CompanyRepository.Find(value.CompanyName);
            var tripTask = TripRepository.Find(value.TripAlternateKey);

            var vehicle = await VehicleRepository.Find(value.VehicleName) ?? await VehicleRepository.Add(new Vehicle { Name = value.VehicleName });

            collision.DateKey = (await calendarTask).Key;
            collision.CompanyKey = (await companyTask).Key;
            collision.VehicleKey = vehicle.Key;

            return collision;
        }
    }
}
