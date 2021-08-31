using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Fired
    {
        public int FiredId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public byte[] OrderImage { get; set; }
        public int? ElementId { get; set; }

        public virtual Element Element { get; set; }
    }
}
