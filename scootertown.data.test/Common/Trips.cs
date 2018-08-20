using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Tests.Common
{
    public class Trips
    {
        readonly List<Trip> _trips = new List<Trip>();

        public Trips()
        {
            var calendars = new Calendars();
            var vehicles = new Vehicles();

            for (int i = 0; i < Length; i++)
            {
                _trips.Add(new Trip
                {
                    AlternateKey = $"ABC{i}",
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

        public Trip this[int index]    // Indexer declaration  
        {
            get
            {
                return _trips[index];
            }
        }
    }
}
