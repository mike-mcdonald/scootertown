using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Tests.Common
{
    public class VehicleTypes
    {
        static readonly List<VehicleType> _vehicleTypes = new List<VehicleType>();

        static VehicleTypes()
        {
            _vehicleTypes.Add(new VehicleType
            {
                Name = "Scooter",
            });
        }

        public VehicleType this[int index]    // Indexer declaration  
        {
            get
            {
                return _vehicleTypes[index];
            }
        }
    }
}
