using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class LegalState
    {
        public int LegalStateId { get; set; }
        public string CourtName { get; set; }
        public string CrimeName { get; set; }
        public string LegalNumberDate { get; set; }
        public string LegalDescraption { get; set; }
        public bool? LagelCase { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Result { get; set; }
        public byte[] RulingImage { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
