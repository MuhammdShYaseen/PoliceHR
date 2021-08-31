using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Lost
    {
        public int LostId { get; set; }
        public DateTime? LostDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int? ElementId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public byte[] OrderImage { get; set; }

        public virtual Element Element { get; set; }
    }
}
