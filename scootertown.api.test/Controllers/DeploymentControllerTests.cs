using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PDX.PBOT.Scootertown.API.Controllers;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.API.Test.Common;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using Xunit;

namespace PDX.PBOT.Scootertown.API.Test.Controllers
{
    public class DeploymentControllerTests : IClassFixture<ControllerFixture>
    {
        private readonly ScootertownDbContext Context;
        private readonly DeploymentRepository DeploymentRepository;
        private readonly CalendarRepository CalendarRepository;
        private readonly CompanyRepository CompanyRepository;
        private readonly NeighborhoodRepository NeighborhoodRepository;
        private readonly PlacementReasonRepository PlacementReasonRepository;
        private readonly RemovalReasonRepository RemovalReasonRepository;
        private readonly VehicleRepository VehicleRepository;
        private readonly VehicleTypeRepository VehicleTypeRepository;

        public DeploymentControllerTests(ControllerFixture fixture)
        {
            Context = fixture.Context;
            DeploymentRepository = fixture.DeploymentRepository;
            CalendarRepository = fixture.CalendarRepository;
            CompanyRepository = fixture.CompanyRepository;
            NeighborhoodRepository = fixture.NeighborhoodRepository;
            PlacementReasonRepository = fixture.PlacementReasonRepository;
            RemovalReasonRepository = fixture.RemovalReasonRepository;
            VehicleRepository = fixture.VehicleRepository;
            VehicleTypeRepository = fixture.VehicleTypeRepository;
        }

        [Fact]
        public async void ShouldUpdateDeployment()
        {
            //Given
            var now = DateTime.Now;
            var companyName = "New One";

            var calendar = await CalendarRepository.Add(now);
            await CompanyRepository.Add(new Company { Name = companyName }, true);

            var dbDeployment = new Deployment
            {
                Key = 1,
                StartDateKey = calendar.Key,
                StartTime = now.TimeOfDay,
                EndDateKey = calendar.Key,
                EndTime = now.TimeOfDay.Add(TimeSpan.FromHours(1))
            };

            await DeploymentRepository.Add(dbDeployment);
            // since we are modifying directly and not scoping, we'll need this
            Context.Entry(dbDeployment).State = EntityState.Detached;

            var controller = new DeploymentController(
                new Mock<ILogger<DeploymentController>>().Object,
                DeploymentRepository,
                CalendarRepository,
                CompanyRepository,
                VehicleRepository
            );

            var deploymentDTO = new DeploymentDTO
            {
                Key = 1,
                StartTime = now,
                EndTime = now.AddHours(2),
                CompanyName = companyName
            };
            //When
            var result = await controller.Update(deploymentDTO.Key, deploymentDTO);
            dbDeployment = await DeploymentRepository.Find(1);

            //Then
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<DeploymentDTO>(viewResult.Value);
            Assert.NotNull(model);
            Assert.Equal(deploymentDTO.EndTime.Value.TimeOfDay, dbDeployment.EndTime);
        }
    }
}
