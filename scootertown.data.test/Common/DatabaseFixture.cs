using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Options;

namespace PDX.PBOT.Scootertown.Data.Tests.Common
{
    public class DatabaseFixture
    {
        public ScootertownDbContext Context { get; private set; }

        public DatabaseFixture()
        {
            DbContextOptions<ScootertownDbContext> options;
            var builder = new DbContextOptionsBuilder<ScootertownDbContext>();
            builder.UseInMemoryDatabase("Data");
            options = builder.Options;

            var context = new ScootertownDbContext(options, new VehicleStoreOptions());

            Context = context;
        }
    }
}
