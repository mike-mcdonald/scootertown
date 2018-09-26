using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Concrete;

namespace PDX.PBOT.Scootertown.Data
{
    public class ScootertownDbContextFactory : IDesignTimeDbContextFactory<ScootertownDbContext>
    {
        public ScootertownDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ScootertownDbContext>();
            builder.UseNpgsql(
                @"Host=localhost;Database=scootertown;Username=scootertownadmin;Password=scootertown",
                o => o.UseNetTopologySuite()
            );
            var options = builder.Options;

            var context = new ScootertownDbContext(options, new VehicleStoreOptions());

            return context;
        }
    }
}
