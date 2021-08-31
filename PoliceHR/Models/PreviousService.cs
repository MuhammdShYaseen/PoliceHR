using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class PreviousService
    {
        public int PreviousServicesId { get; set; }
        public string ServiceName { get; set; }
        public string Location { get; set; }
        public string FoundationName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
