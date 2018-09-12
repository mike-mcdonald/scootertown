using System;
using GeoAPI.Geometries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.API.Controllers;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.API.Test.Common;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Tests.Common;
using PDX.PBOT.Scootertown.Infrastructure.Extensions;
using Xunit;

namespace PDX.PBOT.Scootertown.API.Test.Controllers
{
    public class CollisionControllerTests : IClassFixture<ControllerFixture>
    {
        private readonly CollisionController Controller;
        private readonly ScootertownDbContext Context;
        private readonly CollisionRepository CollisionRepository;
        private readonly CalendarRepository CalendarRepository;
        private readonly CompanyRepository CompanyRepository;
        private readonly VehicleRepository VehicleRepository;
        private readonly VehicleTypeRepository VehicleTypeRepository;
        private readonly StatusRepository StatusRepository;
        private readonly TripRepository TripRepository;
        private readonly Calendars Calendars = new Calendars();
        private readonly Companies Companies = new Companies();
        private readonly Trips Trips = new Trips();
        private readonly Vehicles Vehicles = new Vehicles();
        private readonly VehicleTypes VehicleTypes = new VehicleTypes();

        public CollisionControllerTests(ControllerFixture fixture)
        {
            Context = fixture.Context;
            CollisionRepository = fixture.CollisionRepository;
            CalendarRepository = fixture.CalendarRepository;
            CompanyRepository = fixture.CompanyRepository;
            VehicleRepository = fixture.VehicleRepository;
            VehicleTypeRepository = fixture.VehicleTypeRepository;
            StatusRepository = fixture.StatusRepository;
            TripRepository = fixture.TripRepository;

            Controller = new CollisionController(
                new Mock<ILogger<CollisionController>>().Object,
                CollisionRepository,
                CalendarRepository,
                CompanyRepository,
                VehicleRepository,
                VehicleTypeRepository,
                TripRepository
            );
        }

        [Fact]
        public async void ShouldAddCollision()
        {
            //Given
            var now = DateTime.Now;

            var calendar = await CalendarRepository.Add(Calendars[0].Date);
            await CompanyRepository.Add(Companies[0], true);
            await TripRepository.Add(Trips[0]);
            await VehicleRepository.Add(Vehicles[0], true);
            var status = await StatusRepository.Add(new Status { Name = "Open" }, true);
            var location = new Point(new Coordinate(-122.3456, 45.67890));

            var collisionDTO = new CollisionDTO
            {
                Date = Calendars[0].Date + now.TimeOfDay,
                Location = location.ToGeoJson<GeoJSON.Net.Geometry.Point>(),
                CompanyName = Companies[0].Name,
                VehicleName = Vehicles[0].Name,
                TripAlternateKey = Trips[0].AlternateKey,
                VehicleTypeKey = 4,
                OtherVehicleTypeKey = 1,
                ClaimStatusKey = Convert.ToByte(status.Key)
            };

            //When
            var result = await Controller.PostAsync(collisionDTO);
            var dbCollision = await CollisionRepository.Find(1);

            //Then
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<CollisionDTO>(viewResult.Value);
            Assert.NotNull(model);
            Assert.Equal(collisionDTO.Date, model.Date);
            Assert.True(dbCollision.FirstSeen == dbCollision.LastSeen);
        }

        [Fact]
        public async void ShouldUpdateCollision()
        {
            //Given
            var now = DateTime.Now;

            var calendar = await CalendarRepository.Add(Calendars[0].Date);
            var company = await CompanyRepository.Add(Companies[0], true);
            var trip = await TripRepository.Add(Trips[0]);
            var vehicle = await VehicleRepository.Add(Vehicles[0], true);
            var status = await StatusRepository.Add(new Status { Name = "Open" }, true);
            var location = new Point(new Coordinate(-122.3456, 45.67890));

            var dbCollision = new Collision
            {
                Key = 1,
                DateKey = calendar.Key,
                Time = now.TimeOfDay,
                CompanyKey = company.Key,
                Location = location,
                VehicleTypeKey = 4
            };

            await CollisionRepository.Add(dbCollision);


            //When
            var collisionDTO = new CollisionDTO
            {
                Date = Calendars[0].Date + now.TimeOfDay,
                Location = location.ToGeoJson<GeoJSON.Net.Geometry.Point>(),
                CompanyName = Companies[0].Name,
                VehicleName = Vehicles[0].Name,
                TripAlternateKey = Trips[0].AlternateKey,
                VehicleTypeKey = 4,
                OtherVehicleTypeKey = 1,
                ClaimStatusKey = Convert.ToByte(status.Key)
            };

            var result = await Controller.PostAsync(collisionDTO);
            dbCollision = await CollisionRepository.Find(1);

            //Then
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<CollisionDTO>(viewResult.Value);
            Assert.NotNull(model);
            Assert.Equal(collisionDTO.Date.TimeOfDay, dbCollision.Time);
        }
    }
}
