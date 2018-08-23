using System;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Models.Facts
{
    public class Trip : ModelBase
    {
        public new long Key { get; set; }
        /// <summary>
        /// Alternate key for this Trip.  This is the Company's Trip ID.
        /// </summary>
        public string AlternateKey { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public LineString Route { get; set; }
        public int Duration { get; set; }
        public int Distance { get; set; }
        public byte Accuracy { get; set; }
        public short SampleRate { get; set; }
        public byte MaxSpeed { get; set; }
        public byte AverageSpeed { get; set; }
        public int StandardCost { get; set; }
        public int ActualCost { get; set; }
        public string ParkingVerification { get; set; }

        // Relationships
        public int VehicleKey { get; set; }
        public Vehicle Vehicle { get; set; }
        public int CompanyKey { get; set; }
        public Company Company { get; set; }
        public int VehicleTypeKey { get; set; }
        public VehicleType VehicleType { get; set; }
        public int StartDateKey { get; set; }
        public Calendar StartDate { get; set; }
        public int EndDateKey { get; set; }
        public Calendar EndDate { get; set; }
        public int? NeighborhoodStartKey { get; set; }
        public Neighborhood NeighborhoodStart { get; set; }
        public int? NeighborhoodEndKey { get; set; }
        public Neighborhood NeighborhoodEnd { get; set; }
        public int PaymentTypeKey { get; set; }
        public PaymentType PaymentType { get; set; }
        public int PaymentAccessKey { get; set; }
        public PaymentType PaymentAccess { get; set; }
    }
}
