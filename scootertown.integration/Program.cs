using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Integration.Managers.Interfaces;
using PDX.PBOT.Scootertown.Integration.Mappings;
using PDX.PBOT.Scootertown.Integration.Options;
using PDX.PBOT.Scootertown.Integration.Services.Implementations;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration
{
    class Program
    {
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
                        cfg.AddProfile(typeof(DeploymentProfile));
                        cfg.AddProfile(typeof(TripProfile));
                        cfg.AddProfile(typeof(GeoJsonProfile));
                    });

                    services.Configure<AreasOfInterest>(config =>
                    {
                        config.NeighborhoodsFile = Path.Combine(
                            context.HostingEnvironment.ContentRootPath,
                            context.Configuration.GetValue<string>("NeighborhoodsFile")
                        );
                    });

                    // database table options
                    var storeOptions = new VehicleStoreOptions();
                    services.AddSingleton(storeOptions);

                    // transient so I can create multiple services for each company
                    services
                        .AddEntityFrameworkNpgsql()
                        .AddDbContext<ScootertownDbContext>(options =>
                        {
                            options.UseNpgsql(
                                context.Configuration.GetConnectionString("postgres"),
                                o => o.UseNetTopologySuite()
                            );
                        }, ServiceLifetime.Transient);

                    // smaller dimension repositories get singletons to get better performance?
                    services.AddSingleton<ICompanyRepository, CompanyRepository>();
                    services.AddSingleton<IPaymentTypeRepository, PaymentTypeRepository>();
                    services.AddSingleton<IPlacementReasonRepository, PlacementReasonRepository>();
                    services.AddSingleton<IRemovalReasonRepository, RemovalReasonRepository>();
                    services.AddSingleton<IVehicleTypeRepository, VehicleTypeRepository>();

                    // larger dimension repositories don't to save memory
                    services.AddTransient<ICalendarRepository, CalendarRepository>();
                    services.AddTransient<INeighborhoodRepository, NeighborhoodRepository>();
                    services.AddTransient<IVehicleRepository, VehicleRepository>();

                    // add generic services for repositories for any geojson we'll read in
                    services.AddTransient<INeighborhoodRepository, NeighborhoodRepository>();

                    // fact repositories 
                    services.AddTransient<ITripRepository, TripRepository>();
                    services.AddTransient<IDeploymentRepository, DeploymentRepository>();

                    services.AddTransient<ITripService, TripService>();
                    services.AddTransient<IDeploymentService, DeploymentService>();
                    services.AddTransient<INeighborhoodService, NeighborhoodService>();

                    services.AddSingleton<ILoggerFactory, LoggerFactory>();

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
