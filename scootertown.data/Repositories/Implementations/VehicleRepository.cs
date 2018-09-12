using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.Data.Repositories.Implementations
{
    public class VehicleRepository : DimensionRepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ScootertownDbContext context, IMemoryCache cache) : base(context, cache)
        {
        }
    }
}
