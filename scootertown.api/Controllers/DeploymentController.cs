using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.App.API.Controllers
{
    [Route("api/[controller]")]
    public class DeploymentController : Controller
    {
        private readonly ILogger Logger;
        private readonly IDeploymentRepository DeploymentRepository;
        private readonly ICalendarRepository CalendarRepository;
        private readonly ICompanyRepository CompanyRepository;
        private readonly INeighborhoodRepository NeighborhoodRepository;
        private readonly IPlacementReasonRepository PlacementReasonRepository;
        private readonly IRemovalReasonRepository RemovalReasonRepository;
        private readonly IVehicleRepository VehicleRepository;
        private readonly IVehicleTypeRepository VehicleTypeRepository;

        public DeploymentController(
            ILogger<DeploymentController> logger,
            IDeploymentRepository deploymentRepository,
            ICalendarRepository calendarRepository,
            ICompanyRepository companyRepository,
            INeighborhoodRepository neighborhoodRepository,
            IPlacementReasonRepository placementReasonRepository,
            IRemovalReasonRepository removalReasonRepository,
            IVehicleRepository vehicleRepository,
            IVehicleTypeRepository vehicleTypeRepository
        )
        {
            Logger = logger;
            DeploymentRepository = deploymentRepository;
            CalendarRepository = calendarRepository;
            CompanyRepository = companyRepository;
            NeighborhoodRepository = neighborhoodRepository;
            PlacementReasonRepository = placementReasonRepository;
            RemovalReasonRepository = removalReasonRepository;
            VehicleRepository = vehicleRepository;
            VehicleTypeRepository = vehicleTypeRepository;
        }

        // GET api/deployment
        [HttpGet]
        public async Task<List<DeploymentDTO>> GetAsync() =>
            (await DeploymentRepository.All()).Select(x => Mapper.Map<DeploymentDTO>(x)).ToList();

        // GET api/deployment/bird/active
        [HttpGet("{company}/active")]
        public async Task<IActionResult> GetActiveAsync(string company)
        {
            try
            {
                var deployments = await DeploymentRepository.GetActive(company);
                return Ok(deployments.Select(x => Mapper.Map<DeploymentDTO>(x)).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // GET api/deployment/5
        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync(long key)
        {
            var deployment = await DeploymentRepository.Find(key);
            return Ok(Mapper.Map<DeploymentDTO>(deployment));
        }

        // POST api/deployment
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DeploymentDTO value)
        {
            var now = DateTime.Now;

            var deployment = await FillDeployment(value);

            await DeploymentRepository.Add(deployment, false);

            try
            {
                await DeploymentRepository.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.LogError("Error adding deployment:\n{message}", e.Message);
                return BadRequest(e.ToString());
            }
            return Ok(Mapper.Map<DeploymentDTO>(deployment));
        }

        // PUT api/deployment/5
        [HttpPut("{key}")]
        public async Task<IActionResult> Update(long key, [FromBody]DeploymentDTO value)
        {
            try
            {
                var deployment = await FillDeployment(value);

                await DeploymentRepository.Update(deployment);
                return Ok(Mapper.Map<DeploymentDTO>(deployment));
            }
            catch (Exception e)
            {
                Logger.LogError("Error updating deployment:\n{message}", e.Message);
                return BadRequest(e.ToString());
            }
        }

        // POST api/deployment/5/end
        [HttpPost("{key}/end")]
        public async Task<IActionResult> EndDeployment(long key)
        {
            Logger.LogInformation($"Ending deployment {key}...");
            try
            {
                var deployment = await DeploymentRepository.Find(key);
                var now = DateTime.Now;
                var calendarTask = CalendarRepository.Find(now);
                deployment.EndTime = now.TimeOfDay;
                deployment.LastSeen = now;
                deployment.EndDateKey = (await calendarTask).Key;

                await DeploymentRepository.Update(deployment);

                return Ok(Mapper.Map<DeploymentDTO>(deployment));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

        private async Task<Deployment> FillDeployment(DeploymentDTO value)
        {
            var deployment = Mapper.Map<Deployment>(value);

            var vehicle = await VehicleRepository.Find(value.Vehicle) ?? await VehicleRepository.Add(new Vehicle { Name = value.Vehicle });
            var endDateTask = value.EndTime.HasValue ? CalendarRepository.Find(value.EndTime.Value) : Task.FromResult<Calendar>(null);

            // need the vehicle so we can test for active deployments
            deployment.VehicleKey = vehicle.Key;

            // Get the reference properties set up
            // write this first so we don't start two operations
            deployment.EndDateKey = (await endDateTask)?.Key;

            var companyTask = CompanyRepository.Find(value.Company);
            var startDateTask = CalendarRepository.Find(value.StartTime);

            deployment.CompanyKey = (await companyTask).Key;
            deployment.StartDateKey = (await startDateTask).Key;

            return deployment;
        }
    }
}
