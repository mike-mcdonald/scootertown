using System.ComponentModel.DataAnnotations.Schema;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Models.Bridges
{
    public class StreetSegmentGroup : ModelBase
    {
        [NotMapped]
        public new int Key { get; set; }
        public long? StreetSegmentGroupKey { get; set; }
        public Dimensions.StreetSegmentGroup SegmentGroup { get; set; }
        public int? StreetSegmentKey { get; set; }
        public StreetSegment StreetSegment { get; set; }
    }
}
