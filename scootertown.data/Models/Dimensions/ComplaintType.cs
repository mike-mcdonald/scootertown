using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class ComplaintType : DimensionBase
    {
        public List<Complaint> Complaints { get; set; }
    }
}
