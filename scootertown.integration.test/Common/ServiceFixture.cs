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
using PDX.PBOT.Scootertown.Integration.Mappings;
using PDX.PBOT.Scootertown.Integration.Services.Implementations;

namespace PDX.PBOT.Scootertown.Integration.Test.Common
{
    public class ServiceFixture : IDisposable
    {
        public readonly DeploymentService DeploymentService;
        public readonly DeploymentRepository DeploymentRepository;
        public readonly CalendarRepository CalendarRepository;
        public readonly CompanyRepository CompanyRepository;
        public readonly NeighborhoodRepository NeighborhoodRepository;
        public readonly PatternAreaRepository PatternAreaRepository;
        public readonly PlacementReasonRepository PlacementReasonRepository;
        public readonly RemovalReasonRepository RemovalReasonRepository;
        public readonly VehicleRepository VehicleRepository;
        public readonly VehicleTypeRepository VehicleTypeRepository;

        public ServiceFixture()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DeploymentProfile>();
            });


            DbContextOptions<ScootertownDbContext> options;
            var builder = new DbContextOptionsBuilder<ScootertownDbContext>();
            builder.UseInMemoryDatabase("Intregration");
            options = builder.Options;

            var context = new ScootertownDbContext(options, new VehicleStoreOptions());

            NeighborhoodRepository = new NeighborhoodRepository(context);
            PatternAreaRepository = new PatternAreaRepository(context);

            DeploymentService = new DeploymentService(
                new Mock<ILogger<DeploymentService>>().Object,
                null,
                NeighborhoodRepository,
                PatternAreaRepository
            );
        }

        public void Dispose()
        {

        }
    }
}
