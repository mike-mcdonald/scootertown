using System;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Options;

namespace PDX.PBOT.Scootertown.Data.Tests.Common
{
    public class DatabaseFixture : IDisposable
    {
        public ScootertownDbContext Context { get; private set; }

        public DatabaseFixture()
        {
            DbContextOptions<ScootertownDbContext> options;
            var builder = new DbContextOptionsBuilder<ScootertownDbContext>();
            builder.UseNpgsql(
                @"Host=localhost;Database=scootertown;Username=scootertownadmin;Password=b43b25iun8fneufniosergigakei0r",
                o => o.UseNetTopologySuite()
            );
            options = builder.Options;

            var context = new ScootertownDbContext(options, new VehicleStoreOptions());

            var connection = ((NpgsqlConnection)context.Database.GetDbConnection());
            if (!context.Database.EnsureCreated())
            {
                connection.Open();
            }
            connection.ReloadTypes();

            Context = context;
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }
    }
}
