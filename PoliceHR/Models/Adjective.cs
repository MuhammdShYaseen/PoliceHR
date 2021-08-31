using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Adjective
    {
        public int AdjectiveId { get; set; }
        public string AdjectiveName { get; set; }
        public int? ElementId { get; set; }

        public virtual Element Element { get; set; }
    }
}
