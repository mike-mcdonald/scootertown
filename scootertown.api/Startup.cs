using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PDX.PBOT.Scootertown.API.Mappings;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace scootertown.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // AutoMapper setup using profiles.
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DeploymentProfile>();
                cfg.AddProfile<TripProfile>();
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
                        Configuration.GetConnectionString("postgres"),
                        o => o.UseNetTopologySuite()
                    );
                }, ServiceLifetime.Transient);

            // smaller dimension repositories get singletons to get better performance?
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IPaymentTypeRepository, PaymentTypeRepository>();
            services.AddTransient<IPlacementReasonRepository, PlacementReasonRepository>();
            services.AddTransient<IRemovalReasonRepository, RemovalReasonRepository>();
            services.AddTransient<IVehicleTypeRepository, VehicleTypeRepository>();

            // larger dimension repositories don't to save memory
            services.AddTransient<ICalendarRepository, CalendarRepository>();
            services.AddTransient<INeighborhoodRepository, NeighborhoodRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();

            // add generic services for repositories for any geojson we'll read in
            services.AddTransient<INeighborhoodRepository, NeighborhoodRepository>();

            // fact repositories 
            services.AddTransient<ITripRepository, TripRepository>();
            services.AddTransient<IDeploymentRepository, DeploymentRepository>();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
