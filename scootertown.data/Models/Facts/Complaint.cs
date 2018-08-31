using System;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Models.Facts
{
    public class Complaint : ModelBase
    {
        private static readonly char delimiter = '|';

        public TimeSpan SubmittedTime { get; set; }
        public Point Location { get; set; }
        public string ComplaintDetails { get; set; }
        public string InternalComplaints { get; set; }
        [NotMapped]
        public string[] Complaints
        {
            get { return InternalComplaints.Split(delimiter); }
            set
            {
                InternalComplaints = string.Join($"{delimiter}", value);
            }
        }

        // references
        public int SubmittedDateKey { get; set; }
        public Calendar SubmittedDate { get; set; }
        public int VehicleKey { get; set; }
        public Vehicle Vehicle { get; set; }
        public int CompanyKey { get; set; }
        public Company Company { get; set; }
        public int VehicleTypeKey { get; set; }
        public VehicleType VehicleType { get; set; }
        public int ComplaintTypeKey { get; set; }
        public ComplaintType ComplaintType { get; set; }
    }
}
