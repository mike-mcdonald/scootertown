using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NLog;
using NLog.Extensions.Logging;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Integration.Infrastructure;
using PDX.PBOT.Scootertown.Integration.Managers;
using PDX.PBOT.Scootertown.Integration.Managers.Interfaces;
using PDX.PBOT.Scootertown.Integration.Mappings;
using PDX.PBOT.Scootertown.Integration.Services.Implementations;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration
{
    class Program
    {
        private static IConfiguration Configuration;
        private static List<ICompanyManager> Managers = new List<ICompanyManager>();
        private static IServiceCollection Services;
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json");
                    Configuration = builder.Build();
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);

                    GeoJsonReader reader = new GeoJsonReader();
                    var area = reader.Read<Polygon>(File.ReadAllText("eastportland.json"));
                    services.AddSingleton(area);
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    logging.AddNLog();

                    NLog.LogManager.LoadConfiguration("nlog.config");
                });
            await host.RunConsoleAsync();
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // AutoMapper setup using profiles.
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(typeof(DeploymentProfile));
                cfg.AddProfile(typeof(TripProfile));
            });

            var storeOptions = new VehicleStoreOptions();
            services.AddSingleton(storeOptions);

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ScootertownDbContext>(options =>
                {
                    options.UseNpgsql(
                        Configuration.GetConnectionString("postgres"),
                        o => o.UseNetTopologySuite()
                    );
                }, ServiceLifetime.Transient);

            services.AddTransient<ICalendarRepository, CalendarRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IPaymentTypeRepository, PaymentTypeRepository>();
            services.AddTransient<IPlacementReasonRepository, PlacementReasonRepository>();
            services.AddTransient<IRemovalReasonRepository, RemovalReasonRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IVehicleTypeRepository, VehicleTypeRepository>();

            services.AddTransient<ITripRepository, TripRepository>();
            services.AddTransient<IDeploymentRepository, DeploymentRepository>();

            services.AddTransient<ITripService, TripService>();
            services.AddTransient<IDeploymentService, DeploymentService>();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            services.AddSingleton<IHostedService, Host>();
        }
    }
}
