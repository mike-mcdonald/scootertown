namespace PDX.PBOT.Scootertown.Data.Options
{
    public class VehicleStoreOptions
    {
        public string DefaultSchema { get; set; } = null;

        public TableConfiguration VehicleGroup { get; set; } = new TableConfiguration("VehicleGroup", "Dim");
        public TableConfiguration VehicleBureau { get; set; } = new TableConfiguration("VehicleBureau", "Dim");
        public TableConfiguration VehicleType { get; set; } = new TableConfiguration("VehicleType", "Dim");
        public TableConfiguration VehicleLocation { get; set; } = new TableConfiguration("VehicleLocation", "Fact");
        public TableConfiguration Vehicle { get; set; } = new TableConfiguration("Vehicle", "Dim");
        public TableConfiguration Calendar { get; set; } = new TableConfiguration("Calendar", "Dim");
    }
}