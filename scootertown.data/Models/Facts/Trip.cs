using System;
using PDX.PBOT. Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Models.Facts
{
    public class Trip {
        public long Key { get; set; }
        public string CompanyKey { get; set; }
        public Time StartTime { get; set; }
        public Time EndTime { get; set; }

        public Calendar StartDate { get; set; }
        public Calendar EndDate { get; set; }
    }
}