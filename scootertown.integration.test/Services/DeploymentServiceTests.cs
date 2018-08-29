using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Tests.Common;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using PDX.PBOT.Scootertown.Infrastructure.JSON;
using PDX.PBOT.Scootertown.Integration.Models;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;
using PDX.PBOT.Scootertown.Integration.Test.Common;
using Xunit;

namespace PDX.PBOT.Scootertown.Integration.Test
{
    public class DeploymentServiceTests : IClassFixture<ServiceFixture>
    {
        private readonly IDeploymentService Service;
        private readonly Calendars Calendars;
        private readonly Vehicles Vehicles;
        private readonly Queue<DeploymentDTO> Deployments;
        public readonly DeploymentRepository DeploymentRepository;
        private readonly CalendarRepository CalendarRepository;
        private readonly CompanyRepository CompanyRepository;
        private readonly PlacementReasonRepository PlacementReasonRepository;
        private readonly RemovalReasonRepository RemovalReasonRepository;
        private readonly VehicleRepository VehicleRepository;
        private readonly VehicleTypeRepository VehicleTypeRepository;


        public DeploymentServiceTests(ServiceFixture fixture)
        {
            Calendars = new Calendars();
            Vehicles = new Vehicles();

            // Arrange
            Service = fixture.DeploymentService;

            DeploymentRepository = fixture.DeploymentRepository;
            CalendarRepository = fixture.CalendarRepository;
            CompanyRepository = fixture.CompanyRepository;
            PlacementReasonRepository = fixture.PlacementReasonRepository;
            RemovalReasonRepository = fixture.RemovalReasonRepository;
            VehicleRepository = fixture.VehicleRepository;
            VehicleTypeRepository = fixture.VehicleTypeRepository;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new SafeGeoJsonConverter());

            Deployments = JsonConvert.DeserializeAnonymousType(File.ReadAllText("sample.deployments.json"), new { availability = new Queue<DeploymentDTO>() }, settings).availability;
        }

        [Fact]
        public async void ShouldSaveTestDeployments()
        {
            await Service.Save("Lime", Deployments);
        }

        [Fact]
        public async void ShouldSaveSameDeployments()
        {
            await Service.Save("Lime", Deployments);
            var count = (await DeploymentRepository.All()).Count;
            await Service.Save("Lime", Deployments);
            Assert.Equal(count, (await DeploymentRepository.All()).Count);
            await Service.Save("Lime", Deployments);
            Assert.Equal(count, (await DeploymentRepository.All()).Count);
        }

        [Fact]
        public async void ShouldUpdateDeployments()
        {
            var deployment = new DeploymentDTO();
            await Service.Save("Lime", Deployments);

            var now = DateTime.Now.ToUniversalTime();

            deployment = Deployments.FirstOrDefault(x => !x.EndTime.HasValue);
            deployment.EndTime = (int)now.ToUnixTimestamp();

            await Service.Save("Lime", new Queue<DeploymentDTO>(new DeploymentDTO[] { deployment }));

            var dbDeployments = await DeploymentRepository.All();
            var dbDeployment = dbDeployments.FirstOrDefault(x =>
                x.Vehicle.Name == deployment.Vehicle
            );

            Assert.Equal(Deployments.Count, dbDeployments.Count); // didn't add a new one
            Assert.NotNull(dbDeployment);
            Assert.Equal((long)now.TimeOfDay.TotalSeconds, dbDeployment.EndTime?.TotalSeconds);
        }
    }
}
