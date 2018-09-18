using System;
using System.IO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        public readonly CollisionRepository CollisionRepository;
        public readonly ComplaintRepository ComplaintRepository;
        public readonly CalendarRepository CalendarRepository;
        public readonly CompanyRepository CompanyRepository;
        public readonly NeighborhoodRepository NeighborhoodRepository;
        public readonly PatternAreaRepository PatternAreaRepository;
        public readonly PaymentTypeRepository PaymentTypeRepository;
        public readonly PlacementReasonRepository PlacementReasonRepository;
        public readonly RemovalReasonRepository RemovalReasonRepository;
        public readonly StatusRepository StatusRepository;
        public readonly VehicleRepository VehicleRepository;
        public readonly VehicleTypeRepository VehicleTypeRepository;

        public ControllerFixture()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DeploymentProfile>();
                cfg.AddProfile<TripProfile>();
                cfg.AddProfile<CollisionProfile>();
                cfg.AddProfile<ComplaintProfile>();
            });


            DbContextOptions<ScootertownDbContext> options;
            var builder = new DbContextOptionsBuilder<ScootertownDbContext>();
            builder.UseInMemoryDatabase("API");
            options = builder.Options;

            var context = new ScootertownDbContext(options, new VehicleStoreOptions());
            var cache = new Mock<IMemoryCache>();
            var entry = new Mock<ICacheEntry>();

            int expectedKey = 1;
            object expectedValue = expectedKey;

            cache
                .Setup(x => x.TryGetValue(It.IsAny<object>(), out expectedValue))
                .Returns(true);
            cache
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(entry.Object);


            DeploymentRepository = new DeploymentRepository(context);
            TripRepository = new TripRepository(context);
            CollisionRepository = new CollisionRepository(context);
            ComplaintRepository = new ComplaintRepository(context);
            CalendarRepository = new CalendarRepository(context, cache.Object);
            CompanyRepository = new CompanyRepository(context, cache.Object);
            NeighborhoodRepository = new NeighborhoodRepository(context, cache.Object);
            PatternAreaRepository = new PatternAreaRepository(context, cache.Object);
            PaymentTypeRepository = new PaymentTypeRepository(context, cache.Object);
            PlacementReasonRepository = new PlacementReasonRepository(context, cache.Object);
            RemovalReasonRepository = new RemovalReasonRepository(context, cache.Object);
            StatusRepository = new StatusRepository(context, cache.Object);
            VehicleRepository = new VehicleRepository(context, cache.Object);
            VehicleTypeRepository = new VehicleTypeRepository(context, cache.Object);

            Context = context;
        }

        public void Dispose()
        {

        }
    }
}
