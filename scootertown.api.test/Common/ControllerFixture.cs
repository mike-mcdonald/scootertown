using System;
using System.IO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.API.Mappings;

namespace PDX.PBOT.Scootertown.API.Test.Common
{
    public class ControllerFixture : IDisposable
    {
        public readonly ScootertownDbContext Context;
        public readonly DeploymentRepository DeploymentRepository;
        public readonly TripRepository TripRepository;
        public readonly CalendarRepository CalendarRepository;
        public readonly CompanyRepository CompanyRepository;
        public readonly NeighborhoodRepository NeighborhoodRepository;
        public readonly PatternAreaRepository PatternAreaRepository;
        public readonly PaymentTypeRepository PaymentTypeRepository;
        public readonly PlacementReasonRepository PlacementReasonRepository;
        public readonly RemovalReasonRepository RemovalReasonRepository;
        public readonly VehicleRepository VehicleRepository;
        public readonly VehicleTypeRepository VehicleTypeRepository;

        public ControllerFixture()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DeploymentProfile>();
                cfg.AddProfile<TripProfile>();
            });


            DbContextOptions<ScootertownDbContext> options;
            var builder = new DbContextOptionsBuilder<ScootertownDbContext>();
            builder.UseInMemoryDatabase("API");
            options = builder.Options;

            var context = new ScootertownDbContext(options, new VehicleStoreOptions());

            DeploymentRepository = new DeploymentRepository(context);
            TripRepository = new TripRepository(context);
            CalendarRepository = new CalendarRepository(context);
            CompanyRepository = new CompanyRepository(context);
            NeighborhoodRepository = new NeighborhoodRepository(context);
            PatternAreaRepository = new PatternAreaRepository(context);
            PaymentTypeRepository = new PaymentTypeRepository(context);
            PlacementReasonRepository = new PlacementReasonRepository(context);
            RemovalReasonRepository = new RemovalReasonRepository(context);
            VehicleRepository = new VehicleRepository(context);
            VehicleTypeRepository = new VehicleTypeRepository(context);

            Context = context;
        }

        public void Dispose()
        {

        }
    }
}
