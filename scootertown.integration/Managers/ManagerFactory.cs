using System;
using Microsoft.Extensions.Configuration;
using PDX.PBOT.Scootertown.Integration.Managers.Implementations;
using PDX.PBOT.Scootertown.Integration.Managers.Interfaces;

namespace PDX.PBOT.Scootertown.Integration.Managers
{
    public static class ManagerFactory
    {
        public static ICompanyManager GetManager(string name, IConfigurationSection configuration)
        {
            switch (name)
            {
                case "Bird":
                    return new BirdManager(configuration);
                case "Lime":
                    return new LimeManager(configuration);
                case "Skip":
                    return new SkipManager(configuration);
                default:
                    throw new Exception($"No manager with name '{name}' found.");
            }
        }
    }
}
