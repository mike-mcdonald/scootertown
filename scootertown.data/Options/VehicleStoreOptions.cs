namespace PDX.PBOT.Scootertown.Data.Options
{
    public class VehicleStoreOptions
    {
        public string DefaultSchema { get; set; } = null;

        public TableConfiguration BridgeStreetSegmentGroup { get; set; } = new TableConfiguration("streetsegmentgroup", "bridge");
        public TableConfiguration BridgeBicyclePathGroup { get; set; } = new TableConfiguration("bicyclepathgroup", "bridge");
        public TableConfiguration DimBicyclePathGroup { get; set; } = new TableConfiguration("bicyclepathgroup", "dim");
        public TableConfiguration BicyclePath { get; set; } = new TableConfiguration("bicyclepath", "dim");
        public TableConfiguration Calendar { get; set; } = new TableConfiguration("calendar", "dim");
        public TableConfiguration Company { get; set; } = new TableConfiguration("company", "dim");
        public TableConfiguration ComplaintType { get; set; } = new TableConfiguration("complainttype", "dim");
        public TableConfiguration Neighborhood { get; set; } = new TableConfiguration("neighborhood", "dim");
        public TableConfiguration PatternArea { get; set; } = new TableConfiguration("patternarea", "dim");
        public TableConfiguration PaymentType { get; set; } = new TableConfiguration("paymenttype", "dim");
        public TableConfiguration PlacementReason { get; set; } = new TableConfiguration("placementreason", "dim");
        public TableConfiguration RemovalReason { get; set; } = new TableConfiguration("removalreason", "dim");
        public TableConfiguration Status { get; set; } = new TableConfiguration("status", "dim");
        public TableConfiguration DimStreetSegmentGroup { get; set; } = new TableConfiguration("streetsegmentgroup", "dim");
        public TableConfiguration StreetSegment { get; set; } = new TableConfiguration("streetsegment", "dim");
        public TableConfiguration Vehicle { get; set; } = new TableConfiguration("vehicle", "dim");
        public TableConfiguration VehicleType { get; set; } = new TableConfiguration("vehicletype", "dim");

        public TableConfiguration Collision { get; set; } = new TableConfiguration("collision", "fact");
        public TableConfiguration Complaint { get; set; } = new TableConfiguration("complaint", "fact");
        public TableConfiguration Deployment { get; set; } = new TableConfiguration("deployment", "fact");
        public TableConfiguration Trip { get; set; } = new TableConfiguration("trip", "fact");
    }
}
