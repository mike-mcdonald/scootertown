namespace PDX.PBOT.Scootertown.Data.Options
{
    public class VehicleStoreOptions
    {
        public string DefaultSchema { get; set; } = null;

        public TableConfiguration Calendar { get; set; } = new TableConfiguration("Calendar", "Dim");
        public TableConfiguration Company { get; set; } = new TableConfiguration("Company", "Dim");
        public TableConfiguration PaymentType { get; set; } = new TableConfiguration("PaymentType", "Dim");
        public TableConfiguration PlacementReason { get; set; } = new TableConfiguration("PlacementReason", "Dim");
        public TableConfiguration RemovalReason { get; set; } = new TableConfiguration("RemovalReason", "Dim");
        public TableConfiguration Vehicle { get; set; } = new TableConfiguration("Vehicle", "Dim");
        public TableConfiguration VehicleType { get; set; } = new TableConfiguration("VehicleType", "Dim");

        public TableConfiguration Collision { get; set; } = new TableConfiguration("Collision", "Fact");
        public TableConfiguration Complaint { get; set; } = new TableConfiguration("Complaint", "Fact");
        public TableConfiguration Deployment { get; set; } = new TableConfiguration("Deployment", "Fact");
        public TableConfiguration Trip { get; set; } = new TableConfiguration("Trip", "Fact");
    }
}
