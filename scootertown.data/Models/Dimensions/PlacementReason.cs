using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class PlacementReason : DimensionBase
    {
        public List<Deployment> Deployments { get; set; }
    }
}
