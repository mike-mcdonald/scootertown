using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PDX.PBOT.Scootertown.Integration.Managers;
using PDX.PBOT.Scootertown.Integration.Managers.Interfaces;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration
{
    public class Host : IHostedService, IDisposable
    {
        private Timer deploymentTimer;
        private Timer tripTimer;
        private readonly ILogger Logger;
        private readonly IServiceProvider ServiceProvider;
        private readonly List<ICompanyManager> Managers = new List<ICompanyManager>();
        private readonly Dictionary<string, bool> TripQueryLock = new Dictionary<string, bool>();
        private readonly Dictionary<string, bool> DeploymentQueryLock = new Dictionary<string, bool>();

        public Host(
            IConfiguration configuration,
            ILogger<Host> logger,
            IServiceProvider serviceProvider
        )
        {
            Logger = logger;
            ServiceProvider = serviceProvider;

            var companySettings = configuration.GetSection("CompanySettings");

            foreach (var setting in companySettings.GetChildren())
            {
                var manager = ManagerFactory.GetManager(setting.Key, setting);
                Managers.Add(manager);
                TripQueryLock.Add(setting.Key, false);
                DeploymentQueryLock.Add(setting.Key, false);
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Starting.");

            Logger.LogDebug("Reading neighborhoods...");
            try
            {
                await ServiceProvider.GetRequiredService<INeighborhoodService>().Save();
            }
            catch (Exception e)
            {
                Logger.LogError("Caught exception reading neighborhood data:\n{message}\n{inner}", e.Message, e.InnerException.Message);
            }

            Logger.LogDebug("Reading pattern areas...");
            try
            {
                await ServiceProvider.GetRequiredService<IPatternAreaService>().Save();
            }
            catch (Exception e)
            {
                Logger.LogError("Caught exception reading pattern area data:\n{message}\n{inner}", e.Message, e.InnerException.Message);
            }

            // set up the offsets to pick up where we left off
            foreach (var manager in Managers)
            {
                var tripService = ServiceProvider.GetRequiredService<ITripService>();
                manager.StartingOffset = await tripService.GetTotalTrips(manager.Company);
            }

            deploymentTimer = new Timer(obj =>
            {
                foreach (var manager in Managers)
                {
                    if (!DeploymentQueryLock[manager.Company])
                    {
                        var deploymentTask = Task.Run(async () =>
                        {
                            DeploymentQueryLock[manager.Company] = true;

                            try
                            {
                                Logger.LogDebug("Retrieving availability for {Company}.", manager.Company);

                                var deployments = await manager.RetrieveAvailability();

                                var deploymentService = ServiceProvider.GetRequiredService<IDeploymentService>();

                                Logger.LogDebug("Writing {count} availability records for {Company}.", deployments.Count, manager.Company);

                                await deploymentService.Save(manager.Company, deployments);

                                Logger.LogDebug("Done writing availability records for {Company}.", manager.Company);
                            }
                            catch (Exception e)
                            {
                                Logger.LogError("Caught exception processing data:\n{message}\n{trace}", e.Message, e.StackTrace);
                            }
                            finally
                            {
                                DeploymentQueryLock[manager.Company] = false;
                            }
                        });
                    }
                }
            }, new AutoResetEvent(false), 1000, 1000 * 20);

            tripTimer = new Timer(obj =>
            {
                try
                {
                    foreach (var manager in Managers)
                    {
                        if (!TripQueryLock[manager.Company])
                        {
                            var tripsTask = Task.Run(async () =>
                            {
                                TripQueryLock[manager.Company] = true;

                                try
                                {
                                    Logger.LogDebug("Retrieving trips for {Company}.", manager.Company);

                                    var trips = await manager.RetrieveTrips();

                                    var tripService = ServiceProvider.GetRequiredService<ITripService>();

                                    Logger.LogDebug("Writing {count} trip records for {Company}.", trips.Count, manager.Company);

                                    await tripService.Save(manager.Company, trips);

                                    Logger.LogDebug("Done writing trip records for {Company}.", manager.Company);
                                }
                                catch (System.Exception e)
                                {
                                    Logger.LogError("Caught exception processing data:\n{message}\n{trace}", e.Message, e.StackTrace);
                                }
                                finally
                                {
                                    TripQueryLock[manager.Company] = false;
                                }
                            });
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Logger.LogError($"Caught exception: {e.Message}");
                }

            }, new AutoResetEvent(false), 1000 * 10, 1000 * 60);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Stopping.");

            deploymentTimer?.Change(Timeout.Infinite, 0);
            tripTimer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            deploymentTimer?.Dispose();
            tripTimer?.Dispose();
        }
    }
}
