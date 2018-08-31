using System;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Models.Facts
{
    public class Collision : ModelBase
    {
        private static readonly char delimiter = '|';

        public TimeSpan Time { get; set; }
        public bool OtherUser { get; set; }
        public bool Helmet { get; set; }
        public Point Location { get; set; }
        public bool Citation { get; set; }
        public string CitationDetails { get; set; }
        public bool Injury { get; set; }
        public bool StateReport { get; set; }
        public string InternalReports { get; set; }
        [NotMapped]
        public string[] Reports
        {
            get { return InternalReports.Split(delimiter); }
            set
            {
                InternalReports = string.Join($"{delimiter}", value);
            }
        }

        // reference properties
        public int DateKey { get; set; }
        public Calendar Date { get; set; }
        public long TripKey { get; set; }
        public Trip Trip { get; set; }
        public int OtherVehicleKey { get; set; }
        public VehicleType OtherVehicle { get; set; }
        public int ClaimStatusKey { get; set; }
        public Status ClaimStatus { get; set; }
    }
}
