using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Tests.Common
{
    public class Vehicles
    {
        readonly List<Vehicle> _vehicles = new List<Vehicle>();

        public Vehicles()
        {
            var vehicleTypes = new VehicleTypes();

            for (int i = 0; i < Length; i++)
            {
                _vehicles.Add(new Vehicle
                {
                    Name = $"ABC{i}",
                    Type = vehicleTypes[0]
                });
            }
        }

        public byte Length
        {
            get
            {
                return 2;
            }
        }

        public Vehicle this[int index]    // Indexer declaration  
        {
            get
            {
                return _vehicles[index];
            }
        }
    }
}
