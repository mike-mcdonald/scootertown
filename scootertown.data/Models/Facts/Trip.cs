using System;
using System.Data.Entity.Spatial;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Models.Facts
{
    public class Trip
    {
        public long Key { get; set; }
        /// <summary>
        /// Alternate key for this Trip.  This is the Company's Trip ID.
        /// </summary>
        public string AlternateKey { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DbGeometry StartPoint { get; set; }
        public DbGeometry EndPoint { get; set; }
        public DbGeometry Route { get; set; }
        public int Duration { get; set; }
        public int Distance { get; set; }
        public byte Accuracy { get; set; }
        public byte SampleRate { get; set; }
        public byte MaxSpeed { get; set; }
        public byte AverageSpeed { get; set; }
        public byte StandardCost { get; set; }
        public byte ActualCost { get; set; }
        public string ParkingVerification { get; set; }

        public Vehicle Vehicle { get; set; }
        public Company Company { get; set; }
        public VehicleType VehicleType { get; set; }
        public Calendar StartDate { get; set; }
        public Calendar EndDate { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentType PaymentAccess { get; set; }
    }
}