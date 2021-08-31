using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Retention
    {
        public int RetentionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ElementId { get; set; }
        public string OrderNum { get; set; }
        public byte[] OrderImage { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Element Element { get; set; }
    }
}
