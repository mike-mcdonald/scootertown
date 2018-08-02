using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Concrete;
using System.Data.Entity.Infrastructure;

namespace PDX.PBOT.Scootertown.Data
{
    public class ScootertownDbContextFactory : IDbContextFactory<ScootertownDbContext>
    {
        private readonly DbContextOptions<ScootertownDbContext> ContextOptions;
        private readonly VehicleStoreOptions StoreOptions;

        public ScootertownDbContextFactory(DbContextOptions<ScootertownDbContext> options, VehicleStoreOptions storeOptions)
        {
            ContextOptions = options;
            StoreOptions = storeOptions;
        }

        public ScootertownDbContext Create()
        {
            throw new NotImplementedException();
        }

        public TDbContext CreateDbContext<TDbContext>() where TDbContext : class, IDbContext
        {
            var context = new ScootertownDbContext(ContextOptions, StoreOptions)as IDbContext;
            return (TDbContext)context;
        }
    }
}