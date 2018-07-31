#if !NET40
using System.Runtime.ExceptionServices;
#endif
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Concrete;
#if EF6
using System.Data.Entity;
#endif
using EntityFramework.DbContextScope.Interfaces;
#if EFCore
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
#elif EF6

#endif

namespace PDX.PBOT.Scootertown.Data
{
    public class NavManDbContextFactory : IDbContextFactory
    {
        private readonly DbContextOptions<NavManDbContext> ContextOptions;
        private readonly VehicleStoreOptions StoreOptions;

        public NavManDbContextFactory(DbContextOptions<NavManDbContext> options, VehicleStoreOptions storeOptions)
        {
            ContextOptions = options;
            StoreOptions = storeOptions;
        }

        public TDbContext CreateDbContext<TDbContext>() where TDbContext : class, IDbContext
        {
            var context = new NavManDbContext(ContextOptions, StoreOptions)as IDbContext;
            return (TDbContext)context;
        }
    }
}