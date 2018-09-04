using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Tests.Common
{
    public class Collisions
    {
        readonly List<Collision> _collisions = new List<Collision>();

        public Collisions()
        {
            var calendars = new Calendars();
            var companies = new Companies();
            var trips = new Trips();
            var vehicles = new Vehicles();
            var vehicleTypes = new VehicleTypes();

            for (int i = 0; i < Length; i++)
            {
                _collisions.Add(new Collision
                {
                    Date = calendars[i % calendars.Length],
                    Time = TimeSpan.FromHours(9),
                    Company = companies[i % companies.Length],
                    Vehicle = vehicles[i % vehicles.Length],
                    VehicleType = vehicleTypes[i % vehicleTypes.Length],
                    OtherVehicleType = vehicleTypes[i % vehicleTypes.Length],
                    Trip = trips[i % trips.Length]
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

        public Collision this[int index]    // Indexer declaration  
        {
            get
            {
                return _collisions[index];
            }
        }
    }
}
