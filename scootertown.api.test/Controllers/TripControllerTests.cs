using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PDX.PBOT.App.API.Controllers;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.API.Test.Common;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using Xunit;

namespace PDX.PBOT.Scootertown.API.Test.Controllers
{
    public class TripControllerTests : IClassFixture<ControllerFixture>
    {
        private readonly ScootertownDbContext Context;
        private readonly TripRepository TripRepository;
        private readonly CalendarRepository CalendarRepository;
        private readonly CompanyRepository CompanyRepository;
        private readonly NeighborhoodRepository NeighborhoodRepository;
        private readonly PatternAreaRepository PatternAreaRepository;
        private readonly PaymentTypeRepository PaymentTypeRepository;
        private readonly VehicleRepository VehicleRepository;
        private readonly VehicleTypeRepository VehicleTypeRepository;

        public TripControllerTests(ControllerFixture fixture)
        {
            Context = fixture.Context;
            TripRepository = fixture.TripRepository;
            CalendarRepository = fixture.CalendarRepository;
            CompanyRepository = fixture.CompanyRepository;
            NeighborhoodRepository = fixture.NeighborhoodRepository;
            PatternAreaRepository = fixture.PatternAreaRepository;
            PaymentTypeRepository = fixture.PaymentTypeRepository;
            VehicleRepository = fixture.VehicleRepository;
            VehicleTypeRepository = fixture.VehicleTypeRepository;
        }

        [Fact]
        public async void ShouldAddTrip()
        {
            //Given
            var now = DateTime.Now;
            var companyName = "New One";

            var calendar = await CalendarRepository.Add(now);
            var company = await CompanyRepository.Add(new Company { Name = companyName }, true);
            var vehicle = await VehicleRepository.Add(new Vehicle { Name = "Vehicle" }, true);
            var vehicleType = await VehicleTypeRepository.Add(new VehicleType { Name = "VehicleType" }, true);
            var neighborhood = await NeighborhoodRepository.Add(new Neighborhood { Name = "Neighborhood" }, true);
            var patternArea = await PatternAreaRepository.Add(new PatternArea { Name = "Pattern Area" }, true);
            var paymentType = await PaymentTypeRepository.Add(new PaymentType { Name = "Payment" }, true);

            var controller = new TripController(
                new Mock<ILogger<TripController>>().Object,
                TripRepository,
                CalendarRepository,
                CompanyRepository,
                NeighborhoodRepository,
                PaymentTypeRepository,
                VehicleRepository,
                VehicleTypeRepository
            );

            var tripDTO = new TripDTO
            {
                StartTime = now,
                EndTime = now.AddHours(2),
                Company = companyName,
                Vehicle = vehicle.Name,
                VehicleType = Convert.ToByte(vehicleType.Key),
                NeighborhoodStart = neighborhood.Key,
                NeighborhoodEnd = neighborhood.Key,
                PatternAreaStartKey = patternArea.Key,
                PatternAreaEndKey = patternArea.Key,
                PaymentType = Convert.ToByte(paymentType.Key),
                PaymentAccess = Convert.ToByte(paymentType.Key)

            };
            //When
            var result = await controller.PostAsync(tripDTO);
            var dbTrip = await TripRepository.Find(1);

            //Then
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<TripDTO>(viewResult.Value);
            Assert.NotNull(model);
            Assert.Equal(tripDTO.EndTime.TimeOfDay, dbTrip.EndTime);
        }

        [Fact]
        public async void ShouldUpdateSameTrip()
        {
            //Given
            var now = DateTime.Now;
            var companyName = "New One";

            var calendar = await CalendarRepository.Add(now);
            var company = await CompanyRepository.Add(new Company { Name = companyName }, true);
            var vehicle = await VehicleRepository.Add(new Vehicle { Name = "Vehicle" }, true);
            var vehicleType = await VehicleTypeRepository.Add(new VehicleType { Name = "VehicleType" }, true);
            var neighborhood = await NeighborhoodRepository.Add(new Neighborhood { Name = "Neighborhood" }, true);
            var patternArea = await PatternAreaRepository.Add(new PatternArea { Name = "Pattern Area" }, true);
            var paymentType = await PaymentTypeRepository.Add(new PaymentType { Name = "Payment" }, true);

            var dbTrip = new Trip
            {
                Key = 1,
                StartDateKey = calendar.Key,
                StartTime = now.TimeOfDay,
                EndDateKey = calendar.Key,
                EndTime = now.TimeOfDay.Add(TimeSpan.FromHours(1)),
                CompanyKey = company.Key,
                VehicleKey = vehicle.Key,
                VehicleTypeKey = vehicleType.Key,
                NeighborhoodStartKey = neighborhood.Key,
                NeighborhoodEndKey = neighborhood.Key,
                PatternAreaStartKey = patternArea.Key,
                PatternAreaEndKey = patternArea.Key,
                PaymentTypeKey = paymentType.Key,
                PaymentAccessKey = paymentType.Key
            };

            await TripRepository.Add(dbTrip);
            var count = await TripRepository.Count();

            var controller = new TripController(
                new Mock<ILogger<TripController>>().Object,
                TripRepository,
                CalendarRepository,
                CompanyRepository,
                NeighborhoodRepository,
                PaymentTypeRepository,
                VehicleRepository,
                VehicleTypeRepository
            );

            var tripDTO = new TripDTO
            {
                StartTime = now,
                EndTime = now.AddHours(2),
                Company = companyName,
                Vehicle = vehicle.Name,
                VehicleType = Convert.ToByte(vehicleType.Key),
                NeighborhoodStart = neighborhood.Key,
                NeighborhoodEnd = neighborhood.Key,
                PatternAreaStartKey = patternArea.Key,
                PatternAreaEndKey = patternArea.Key,
                PaymentType = Convert.ToByte(paymentType.Key),
                PaymentAccess = Convert.ToByte(paymentType.Key)

            };
            //When
            var result = await controller.PostAsync(tripDTO);
            dbTrip = await TripRepository.Find(1);

            //Then
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<TripDTO>(viewResult.Value);
            Assert.Equal(count, await TripRepository.Count());
            Assert.NotNull(model);
            Assert.Equal(dbTrip.Key, model.Key);
            Assert.Equal(tripDTO.EndTime.TimeOfDay, dbTrip.EndTime);
        }
    }
}
