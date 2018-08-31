using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Tests.Common
{
    public class Deployments
    {
        readonly List<Deployment> _deployments = new List<Deployment>();

        public Deployments()
        {
            var calendars = new Calendars();
            var vehicles = new Vehicles();

            for (int i = 0; i < Length; i++)
            {
                _deployments.Add(new Deployment
                {
                    StartDate = calendars[i % calendars.Length],
                    StartTime = TimeSpan.FromHours(9),
                    EndDate = calendars[i % calendars.Length],
                    EndTime = TimeSpan.FromHours(10),
                    Vehicle = vehicles[i % vehicles.Length]
                });
            }
        }

        public byte Length
        {
            get
            {
                return 20;
            }
        }

        public Deployment this[int index]    // Indexer declaration  
        {
            get
            {
                return _deployments[index];
            }
        }
    }
}
