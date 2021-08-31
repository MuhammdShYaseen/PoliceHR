using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Cycle
    {
        public int CycleId { get; set; }
        public string CycleName { get; set; }
        public DateTime? CycleStartDate { get; set; }
        public DateTime? CycleEndDate { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public byte[] OrderImage { get; set; }
        public string Descraption { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
