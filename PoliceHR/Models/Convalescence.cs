using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Convalescence
    {
        public int ConvalescenceId { get; set; }
        public string Reason { get; set; }
        public string MedicReport { get; set; }
        public byte[] MedicReportImage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsReview { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
