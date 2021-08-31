using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Martyr
    {
        public int MartyrId { get; set; }
        public string Reason { get; set; }
        public string Location { get; set; }
        public DateTime? MartyrDate { get; set; }
        public string OrderNumber { get; set; }
        public byte[] OrderImage { get; set; }
        public int? ElementId { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Element Element { get; set; }
    }
}
