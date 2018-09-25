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
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace PDX.PBOT.Scootertown.Integration
{
    public class Host : IHostedService, IDisposable
    {
        private Timer DeploymentTimer;
        private Timer TripTimer;
        private Timer CollisionTimer;
        private Timer ComplaintTimer;
        private readonly ILogger Logger;
        private readonly Container Container;
        private readonly List<ICompanyManager> Managers = new List<ICompanyManager>();
        private readonly Dictionary<string, bool> TripQueryLock = new Dictionary<string, bool>();
        private readonly Dictionary<string, bool> DeploymentQueryLock = new Dictionary<string, bool>();

        public Host(
            IConfiguration configuration,
            ILogger<Host> logger,
            Container container
        )
        {
            Logger = logger;
            Container = container;

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
                using (AsyncScopedLifestyle.BeginScope(Container))
                {
                    await Container.GetInstance<INeighborhoodService>().Save();
                }
            }
            catch (Exception e)
            {
                Logger.LogError("Caught exception reading neighborhood data:\n{message}\n{inner}", e.Message, e.InnerException?.Message);
            }

            Logger.LogDebug("Reading pattern areas...");
            try
            {
                using (AsyncScopedLifestyle.BeginScope(Container))
                {
                    await Container.GetInstance<IPatternAreaService>().Save();
                }
            }
            catch (Exception e)
            {
                Logger.LogError("Caught exception reading pattern area data:\n{message}\n{inner}", e.Message, e.InnerException?.Message);
            }


            Logger.LogDebug("Reading street segements...");
            try
            {
                using (AsyncScopedLifestyle.BeginScope(Container))
                {
                    await Container.GetInstance<IStreetSegmentService>().Save();
                }
            }
            catch (Exception e)
            {
                Logger.LogError("Caught exception reading street segment data:\n{message}\n{inner}", e.Message, e.InnerException?.Message);
            }

            Logger.LogDebug("Reading bicycle paths...");
            try
            {
                using (AsyncScopedLifestyle.BeginScope(Container))
                {
                    await Container.GetInstance<IBicyclePathService>().Save();
                }
            }
            catch (Exception e)
            {
                Logger.LogError("Caught exception reading bicycle path data:\n{message}\n{inner}", e.Message, e.InnerException?.Message);
            }

            DeploymentTimer = new Timer(obj =>
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

                                using (AsyncScopedLifestyle.BeginScope(Container))
                                {
                                    var deploymentService = Container.GetInstance<IDeploymentService>();

                                    Logger.LogDebug("Writing {count} availability records for {Company}.", deployments.Count, manager.Company);

                                    await deploymentService.Save(manager.Company, deployments);
                                }

                                Logger.LogDebug("Done writing availability records for {Company}.", manager.Company);
                            }
                            catch (Exception e)
                            {
                                Logger.LogError("Caught exception processing data:\n{message}\n{inner}\n{trace}", e.Message, e.InnerException?.Message, e.StackTrace);
                            }
                            finally
                            {
                                DeploymentQueryLock[manager.Company] = false;
                            }
                        });
                    }
                }
            }, new AutoResetEvent(false), 1000, 1000 * 20);

            TripTimer = new Timer(obj =>
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

                                    using (AsyncScopedLifestyle.BeginScope(Container))
                                    {
                                        var tripService = Container.GetInstance<ITripService>();

                                        var trips = await manager.RetrieveTrips();

                                        if (trips.Count == 0)
                                        {
                                            Logger.LogDebug("Resetting offset for {company}.", manager.Company);
                                            manager.Offset = 0;
                                        }

                                        Logger.LogDebug("Writing {count} trip records for {Company}.", trips.Count, manager.Company);

                                        await tripService.Save(manager.Company, trips);
                                    }

                                    Logger.LogDebug("Done writing trip records for {Company}.", manager.Company);
                                }
                                catch (System.Exception e)
                                {
                                    Logger.LogError("Caught exception processing data:\n{message}\n{inner}\n{trace}", e.Message, e.InnerException?.Message, e.StackTrace);
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

            CollisionTimer = new Timer(obj =>
            {
                foreach (var manager in Managers)
                {
                    var collisionTask = Task.Run(async () =>
                    {
                        try
                        {
                            Logger.LogDebug("Retrieving Collisions for {Company}.", manager.Company);

                            var collisions = await manager.RetrieveCollisions();

                            using (AsyncScopedLifestyle.BeginScope(Container))
                            {
                                var collisionService = Container.GetInstance<ICollisionService>();

                                Logger.LogDebug("Writing {count} Collision records for {Company}.", collisions.Count, manager.Company);

                                await collisionService.Save(manager.Company, collisions);
                            }

                            Logger.LogDebug("Done writing Collisions records for {Company}.", manager.Company);
                        }
                        catch (Exception e)
                        {
                            Logger.LogError("Caught exception processing data:\n{message}\n{inner}\n{trace}", e.Message, e.InnerException?.Message, e.StackTrace);
                        }
                    });
                }
            }, new AutoResetEvent(false), 1000 * 60, 1000 * 60 * 60);

            ComplaintTimer = new Timer(obj =>
            {
                foreach (var manager in Managers)
                {
                    var collisionTask = Task.Run(async () =>
                {
                    try
                    {
                        Logger.LogDebug("Retrieving complaints for {Company}.", manager.Company);

                        var complaints = await manager.RetrieveComplaints();

                        using (AsyncScopedLifestyle.BeginScope(Container))
                        {
                            var complaintservice = Container.GetInstance<IComplaintService>();

                            Logger.LogDebug("Writing {count} complaint records for {Company}.", complaints.Count, manager.Company);

                            await complaintservice.Save(manager.Company, complaints);
                        }

                        Logger.LogDebug("Done writing complaints records for {Company}.", manager.Company);
                    }
                    catch (Exception e)
                    {
                        Logger.LogError("Caught exception processing data:\n{message}\n{inner}\n{trace}", e.Message, e.InnerException?.Message, e.StackTrace);
                    }
                });
                }
            }, new AutoResetEvent(false), 0, 1000 * 60 * 60);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Stopping.");

            DeploymentTimer?.Change(Timeout.Infinite, 0);
            TripTimer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            DeploymentTimer?.Dispose();
            TripTimer?.Dispose();
        }
    }
}
