using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Integration.Infrastructure;
using PDX.PBOT.Scootertown.Integration.Models;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration.Services.Implementations
{
    public class DeploymentService : ServiceBase<DeploymentDTO, Deployment>, IDeploymentService
    {
        private readonly ILogger Logger;
        private readonly IDeploymentRepository DeploymentRepository;
        private readonly ICompanyRepository CompanyRepository;
        private readonly INeighborhoodRepository NeighborhoodRepository;
        private readonly IPlacementReasonRepository PlacementReasonRepository;
        private readonly IRemovalReasonRepository RemovalReasonRepository;
        private readonly IVehicleRepository VehicleRepository;
        private readonly IVehicleTypeRepository VehicleTypeRepository;

        private readonly Polygon EastPortland;

        public DeploymentService(
            ILogger<DeploymentService> logger,
            IDeploymentRepository deploymentRepository,
            ICalendarRepository calendarRepository,
            ICompanyRepository companyRepository,
            INeighborhoodRepository neighborhoodRepository,
            IPlacementReasonRepository placementReasonRepository,
            IRemovalReasonRepository removalReasonRepository,
            IVehicleRepository vehicleRepository,
            IVehicleTypeRepository vehicleTypeRepository
        ) : base(calendarRepository)
        {
            Logger = logger;
            DeploymentRepository = deploymentRepository;
            CompanyRepository = companyRepository;
            NeighborhoodRepository = neighborhoodRepository;
            PlacementReasonRepository = placementReasonRepository;
            RemovalReasonRepository = removalReasonRepository;
            VehicleRepository = vehicleRepository;
            VehicleTypeRepository = vehicleTypeRepository;
        }

        public override async Task Save(Queue<DeploymentDTO> items)
        {
            var now = DateTime.Now.ToUniversalTime();

            // get all the active deployments
            var activeDeployments = await DeploymentRepository.GetActive();

            foreach (var item in items)
            {
                try
                {
                    // Use automapper for the original properties
                    var deployment = Mapper.Map<Deployment>(item);

                    // need the vehicle so we can test for active deployments
                    deployment.VehicleKey = (await FindOrAdd<Vehicle>(VehicleRepository, item.Vehicle, new Vehicle { Name = item.Vehicle })).Key;

                    // for this vehicle, is there an active deployment?
                    var currentDeployment = activeDeployments.FirstOrDefault(x => x.VehicleKey == deployment.VehicleKey);
                    if (currentDeployment != null)
                    {
                        Logger.LogTrace("Updating active deployment {key}", currentDeployment.Key);
                        // if there is, update LastSeen
                        currentDeployment.LastSeen = now;
                        // only other thing that we should track as changed is a possible end time
                        currentDeployment.EndDateKey = item.EndTime.HasValue ? (await FindOrAddCalendar(item.EndTime.Value)).Key : (int?)null;
                        currentDeployment.EndTime = deployment.EndTime;
                        await DeploymentRepository.Update(currentDeployment, false);
                        // remove from active list
                        activeDeployments.Remove(currentDeployment);
                    }
                    else
                    {
                        // if there isn't start a new one
                        Logger.LogTrace("Creating new deployment record for vehicle {key}", deployment.VehicleKey);

                        // Get the reference properties set up
                        deployment.CompanyKey = (await FindOrAdd<Company>(CompanyRepository, item.Company, new Company { Name = item.Company })).Key;
                        deployment.StartDateKey = (await FindOrAddCalendar(item.StartTime)).Key;
                        deployment.EndDateKey = item.EndTime.HasValue ? (await FindOrAddCalendar(item.EndTime.Value)).Key : (int?)null;
                        deployment.PlacementReasonKey = (await FindOrAdd<PlacementReason>(PlacementReasonRepository, item.PlacementReason, new PlacementReason { Key = item.PlacementReason })).Key;
                        deployment.PickupReasonKey = (await FindOrAdd<RemovalReason>(RemovalReasonRepository, item.PickupReason, new RemovalReason { Key = item.PickupReason })).Key;
                        deployment.VehicleTypeKey = (await FindOrAdd<VehicleType>(VehicleTypeRepository, item.VehicleType, new VehicleType { Key = item.VehicleType })).Key;

                        deployment.NeighborhoodKey = (await NeighborhoodRepository.Find(deployment.Location))?.Key;

                        deployment.FirstSeen = deployment.LastSeen = now;
                        await DeploymentRepository.Add(deployment, false);
                    }
                }
                catch (DbUpdateException e)
                {
                    // this might be because we are seeing something for the first time
                    Logger.LogWarning("Failed to save a deployment record:\n{message}\n{inner}", e.Message, e.InnerException.Message);
                }
            }

            // for all the other active deployments that we didn't see, update the end time to now
            var endDateKey = (await FindOrAddCalendar(now)).Key;
            foreach (var deploymentToEnd in activeDeployments)
            {
                try
                {
                    deploymentToEnd.LastSeen = now;
                    deploymentToEnd.EndDateKey = endDateKey;
                    deploymentToEnd.EndTime = now.TimeOfDay;
                    await DeploymentRepository.Update(deploymentToEnd, false);
                }
                catch (DbUpdateException e)
                {
                    // this might be because we are seeing something for the first time
                    Logger.LogWarning("Failed to save a deployment record:\n{message}\n{inner}", e.Message, e.InnerException.Message);
                }
            }

            await DeploymentRepository.SaveChanges();
        }
    }
}
