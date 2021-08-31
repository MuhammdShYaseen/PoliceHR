using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Penalty
    {
        public int PenaltiesId { get; set; }
        public string PenaltiesReason { get; set; }
        public string Rank { get; set; }
        public string Name { get; set; }
        public int? PenaltieType { get; set; }
        public string PenaltieNumber { get; set; }
        public DateTime? PenaltieDate { get; set; }
        public byte[] PenaltieImage { get; set; }
        public string PenaltieNotes { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
