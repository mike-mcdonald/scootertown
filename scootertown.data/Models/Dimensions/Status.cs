using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class Status : DimensionBase
    {
        public List<Collision> Collisions { get; set; }
    }
}
