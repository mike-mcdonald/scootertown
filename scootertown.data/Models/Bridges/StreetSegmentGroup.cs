using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Models.Bridges
{
    public class StreetSegmentGroup
    {
        public int? StreetSegmentGroupKey { get; set; }
        public Dimensions.StreetSegmentGroup SegmentGroup { get; set; }
        public int? StreetSegmentKey { get; set; }
        public StreetSegment StreetSegment { get; set; }
    }
}
