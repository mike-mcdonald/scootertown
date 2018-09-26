using System.ComponentModel.DataAnnotations.Schema;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Models.Bridges
{
    public class BicyclePathGroup : ModelBase
    {
        [NotMapped]
        public new int Key { get; set; }
        public long? BicyclePathGroupKey { get; set; }
        public Dimensions.BicyclePathGroup PathGroup { get; set; }
        public int? BicyclePathKey { get; set; }
        public BicyclePath BicyclePath { get; set; }
    }
}
