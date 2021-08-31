using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class MilitaryConscription
    {
        public int MilitaryConscriptionId { get; set; }
        public string CycleNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ElementId { get; set; }

        public virtual Element Element { get; set; }
    }
}
