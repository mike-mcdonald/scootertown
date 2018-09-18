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
    public class ComplaintControllerTests : IClassFixture<ControllerFixture>
    {
        private readonly ComplaintController Controller;
        private readonly ScootertownDbContext Context;
        private readonly ComplaintRepository ComplaintRepository;
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

        public ComplaintControllerTests(ControllerFixture fixture)
        {
            Context = fixture.Context;
            ComplaintRepository = fixture.ComplaintRepository;
            CalendarRepository = fixture.CalendarRepository;
            CompanyRepository = fixture.CompanyRepository;
            VehicleRepository = fixture.VehicleRepository;
            VehicleTypeRepository = fixture.VehicleTypeRepository;
            StatusRepository = fixture.StatusRepository;
            TripRepository = fixture.TripRepository;

            Controller = new ComplaintController(
                new Mock<ILogger<ComplaintController>>().Object,
                ComplaintRepository,
                CalendarRepository,
                CompanyRepository,
                VehicleRepository
            );
        }

        [Fact]
        public async void ShouldAddComplaint()
        {
            //Given
            var now = DateTime.Now;

            var calendar = await CalendarRepository.Add(Calendars[0].Date);
            await CompanyRepository.Add(Companies[0], true);
            await TripRepository.Add(Trips[0]);
            await VehicleRepository.Add(Vehicles[0], true);
            var status = await StatusRepository.Add(new Status { Name = "Open" }, true);
            var location = new Point(new Coordinate(-122.3456, 45.67890));

            var complaintDTO = new ComplaintDTO
            {
                SubmittedDate = Calendars[0].Date + now.TimeOfDay,
                Location = location.ToGeoJson<GeoJSON.Net.Geometry.Point>(),
                CompanyName = Companies[0].Name,
                VehicleName = Vehicles[0].Name,
                VehicleTypeKey = 1,
                ComplaintTypeKey = 8,
                ComplaintDetails = "Vehicle component issue",
                Complaints = new string[] { }
            };

            //When
            var result = await Controller.PostAsync(complaintDTO);
            var dbCollision = await ComplaintRepository.Find(1);

            //Then
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ComplaintDTO>(viewResult.Value);
            Assert.NotNull(model);
            Assert.Equal(complaintDTO.SubmittedDate, model.SubmittedDate);
            Assert.True(dbCollision.FirstSeen == dbCollision.LastSeen);
        }


        [Fact]
        public async void ShouldAddComplaintWithNoVehicle()
        {
            //Given
            var now = DateTime.Now;

            var calendar = await CalendarRepository.Add(Calendars[0].Date);
            await CompanyRepository.Add(Companies[0], true);
            await TripRepository.Add(Trips[0]);
            var status = await StatusRepository.Add(new Status { Name = "Open" }, true);
            var location = new Point(new Coordinate(-122.3456, 45.67890));

            var complaintDTO = new ComplaintDTO
            {
                SubmittedDate = Calendars[0].Date + now.TimeOfDay,
                Location = location.ToGeoJson<GeoJSON.Net.Geometry.Point>(),
                CompanyName = Companies[0].Name,
                VehicleTypeKey = 1,
                ComplaintTypeKey = 8,
                ComplaintDetails = "Vehicle component issue",
                Complaints = new string[] { }
            };

            //When
            var result = await Controller.PostAsync(complaintDTO);
            var dbCollision = await ComplaintRepository.Find(1);

            //Then
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ComplaintDTO>(viewResult.Value);
            Assert.NotNull(model);
            Assert.Equal(complaintDTO.SubmittedDate, model.SubmittedDate);
            Assert.True(dbCollision.FirstSeen == dbCollision.LastSeen);
        }
    }
}
