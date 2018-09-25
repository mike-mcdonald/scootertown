using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Extensions.Logging;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Infrastructure.Mappings;
using PDX.PBOT.Scootertown.Integration.Managers.Interfaces;
using PDX.PBOT.Scootertown.Integration.Mappings;
using PDX.PBOT.Scootertown.Integration.Options;
using PDX.PBOT.Scootertown.Integration.Services.Implementations;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace PDX.PBOT.Scootertown.Integration
{
    public class LoggingAdapter<T> : ILogger<T>
    {
        private readonly ILogger adaptee;

        public LoggingAdapter(ILoggerFactory factory)
        {
            adaptee = factory.CreateLogger<T>();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return adaptee.BeginScope(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return adaptee.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            adaptee.Log(logLevel, eventId, state, exception, formatter);
        }
    }

    class Program
    {
        private static ILoggerFactory ConfigureLogger()
        {
            LoggerFactory factory = new LoggerFactory();

            factory.AddNLog();

            return factory;
        }

        static async Task Main(string[] args)
        {
            var pathToContentRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var host = new HostBuilder()
                /* set cwd to the path we are executing in since
                 *  running 'dotnet run' would otherwise
                 *  set cwd to the folder it is executing from
                 *  and the json files we need are being copied to the output directory
                 */
                .UseContentRoot(pathToContentRoot)
                .ConfigureHostConfiguration((builder) =>
                {
                    builder.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    // AutoMapper setup using profiles.
                    Mapper.Initialize(cfg =>
                    {
                        cfg.AddProfile<DeploymentProfile>();
                        cfg.AddProfile<TripProfile>();
                        cfg.AddProfile<CollisionProfile>();
                        cfg.AddProfile<ComplaintProfile>();
                        cfg.AddProfile<NeighborhoodProfile>();
                        cfg.AddProfile<PatternAreaProfile>();
                        cfg.AddProfile<StreetSegmentProfile>();
                        cfg.AddProfile<BicyclePathProfile>();
                        cfg.AddProfile<GeoJsonProfile>();
                    });

                    // use a different container with more features than the default .NET Core one
                    var container = new Container();
                    container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

                    container.Register<ScootertownDbContext>(() =>
                    {
                        var builder = new DbContextOptionsBuilder<ScootertownDbContext>();
                        builder.UseNpgsql(
                            context.Configuration.GetConnectionString("postgres"),
                            o => o.UseNetTopologySuite()
                        );
                        var options = builder.Options;

                        var dbContext = new ScootertownDbContext(
                            options,
                            new VehicleStoreOptions());

                        return dbContext;
                    }, Lifestyle.Scoped);

                    container.Register<AreasOfInterest>(() =>
                    {
                        var options = new AreasOfInterest
                        {
                            NeighborhoodsFile = Path.Combine(
                                context.HostingEnvironment.ContentRootPath,
                                context.Configuration.GetValue<string>("NeighborhoodsFile")
                            ),
                            PatternAreasFile = Path.Combine(
                                context.HostingEnvironment.ContentRootPath,
                                context.Configuration.GetValue<string>("PatternAreasFile")
                            ),
                            StreetSegmentsFile = Path.Combine(
                                context.HostingEnvironment.ContentRootPath,
                                context.Configuration.GetValue<string>("StreetSegmentsFile")
                            ),
                            BicyclePathsFile = Path.Combine(
                                context.HostingEnvironment.ContentRootPath,
                                context.Configuration.GetValue<string>("BicyclePathsFile")
                            )
                        };
                        return options;
                    }, Lifestyle.Scoped);

                    container.Register<APIOptions>(() =>
                    {
                        var options = new APIOptions
                        {
                            BaseAddress = context.Configuration.GetValue<string>("BaseAddress")
                        };

                        return options;
                    }, Lifestyle.Scoped);

                    // add generic services for repositories for any geojson we'll read in
                    container.Register<INeighborhoodRepository, NeighborhoodRepository>(Lifestyle.Scoped);
                    container.Register<IPatternAreaRepository, PatternAreaRepository>(Lifestyle.Scoped);
                    container.Register<IStreetSegmentRepository, StreetSegmentRepository>(Lifestyle.Scoped);
                    container.Register<IStreetSegmentGroupRepository, StreetSegmentGroupRepository>(Lifestyle.Scoped);
                    container.Register<IBicyclePathRepository, BicyclePathRepository>(Lifestyle.Scoped);
                    container.Register<IBicyclePathGroupRepository, BicyclePathGroupRepository>(Lifestyle.Scoped);

                    container.Register<ITripService, TripService>(Lifestyle.Scoped);
                    container.Register<IDeploymentService, DeploymentService>(Lifestyle.Scoped);
                    container.Register<ICollisionService, CollisionService>(Lifestyle.Scoped);
                    container.Register<IComplaintService, ComplaintService>(Lifestyle.Scoped);
                    container.Register<INeighborhoodService, NeighborhoodService>(Lifestyle.Scoped);
                    container.Register<IPatternAreaService, PatternAreaService>(Lifestyle.Scoped);
                    container.Register<IStreetSegmentService, StreetSegmentService>(Lifestyle.Scoped);
                    container.Register<IBicyclePathService, BicyclePathService>(Lifestyle.Scoped);

                    container.Register(ConfigureLogger, Lifestyle.Singleton);

                    container.Register(typeof(ILogger<>), typeof(LoggingAdapter<>), Lifestyle.Scoped);

                    container.Register<IMemoryCache>(() =>
                    {
                        return new MemoryCache(new MemoryCacheOptions());
                    }, Lifestyle.Singleton);

                    container.Verify();

                    // use the default DI to manage our new container
                    services.AddSingleton(container);

                    services.AddSingleton<ILoggerFactory, LoggerFactory>();

                    // tell the DI container to start the application
                    services.AddSingleton<IHostedService, Host>();
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    logging.AddNLog();

                    NLog.LogManager.LoadConfiguration("nlog.config");
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                });
            await host.RunConsoleAsync();
        }
    }
}
