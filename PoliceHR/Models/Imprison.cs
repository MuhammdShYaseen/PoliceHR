using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Imprison
    {
        public int ImprisonId { get; set; }
        public string Reason { get; set; }
        public string ImprisonEntity { get; set; }
        public DateTime? ImprisonDate { get; set; }
        public string OrderNumber { get; set; }
        public byte[] OrderImage { get; set; }
        public int? ElementId { get; set; }
        public DateTime? EndDate { get; set; }
        public string EndOrderNum { get; set; }

        public virtual Element Element { get; set; }
    }
}
